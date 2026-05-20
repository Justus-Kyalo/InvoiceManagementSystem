using System.Net;
using System.Text;
using FluentValidation;
using InvoiceManagementSystemAPI;
using InvoiceManagementSystemAPI.Data;
using InvoiceManagementSystemAPI.Filters;
using InvoiceManagementSystemAPI.Models;
using InvoiceManagementSystemAPI.Repository;
using InvoiceManagementSystemAPI.Repository.IRepository;
using InvoiceManagementSystemAPI.Services;
using InvoiceManagementSystemAPI.Services.IServices;
using InvoiceManagementSystemAPI.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
});

// Add CORS configuration
var MyCorsPolicy = "AllowLocalhost5173";
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyCorsPolicy,
        policy =>
        {
            policy.WithOrigins("http://localhost:5173", "https://localhost:5173").AllowAnyHeader().AllowAnyMethod();
        });
});

builder.Services.AddControllers(option => { }).AddNewtonsoftJson();
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddScoped<ISlipRepository, SlipRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IIIFBackupRepository, IIFBackupRepository>();
builder.Services.AddScoped<IIIFGeneratorService, IIFGeneratorService>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<ICustomerItemPriceRepository, CustomerItemPriceRepository>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<ISlipDetailRepository, SlipDetailRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddValidatorsFromAssemblyContaining<RegistrationRequestValidator>();

// Authentication set up
var jwtSecretKey = builder.Configuration["ApiSettings:Secret"];

builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = !builder.Environment.IsDevelopment();
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey)),
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["ApiSettings:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["ApiSettings:Audience"],
            ClockSkew = TimeSpan.Zero
        };
        x.Events = new JwtBearerEvents
        {
            OnChallenge = async context =>
            {
                context.HandleResponse();
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                var response = new APIResponse()
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    Errors = new List<string>() { "You are not authenticated. Please login to access this resource." }
                };
                await context.Response.WriteAsJsonAsync(response);
            },
            OnForbidden = async context =>
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "application/json";

                var response = new APIResponse
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    Errors = new List<string> { "You do not have permission to access this resource." }
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        };
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAuthorizationBuilder()
    .SetFallbackPolicy(new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme()
        {
            Description = "\"JWT Authorization header using the Bearer Scheme. \r\n\r\n" +
                          "Enter 'Bearer' [space] and then token in the text input below .\r\n\r\n" +
                          "Example:\"Bearer 1234abcdef\"",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Scheme = "Bearer"
        });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                Scheme = "oauth",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });

    options.OperationFilter<AuthResponseOperationFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Enabled Swagger in production  
// app.UseSwagger();
// app.UseSwaggerUI(options =>
// {
//     options.SwaggerEndpoint("/swagger/v1/swagger.json", "InvoiceManagementSystemAPI_V1");
//     options.RoutePrefix = string.Empty;
// });

// Enable CORS with the policy
app.UseCors(MyCorsPolicy);

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
