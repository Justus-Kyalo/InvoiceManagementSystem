using System.Net;
using FluentValidation;
using FluentValidation.Results;
using InvoiceManagementSystemAPI.Models;
using InvoiceManagementSystemAPI.Models.Dto;
using InvoiceManagementSystemAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManagementSystemAPI.Controllers;
[Route("api/UsersAuth")]
[ApiController]
[AllowAnonymous]
public class UsersController : ControllerBase
{
    protected APIResponse _response;
    private readonly IUserRepository _userRepo;
    private readonly IValidator<RegistrationRequestDTO> _registrationValidator;

    public UsersController(
        IUserRepository userRepo,
        IValidator<RegistrationRequestDTO> registrationValidator)
    {
        _userRepo = userRepo;
        _registrationValidator = registrationValidator;
        _response = new();
    }

    [HttpPost("login")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequestDTO model)
    {
        var loginResponse = await _userRepo.Login(model);

        if (loginResponse.User == null ||
            string.IsNullOrEmpty(loginResponse.Token))
        {
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.Errors.Add("Username or Password is Incorrect");

            return BadRequest(_response);
        }

        _response.StatusCode = HttpStatusCode.OK;
        _response.Result = loginResponse;

        return Ok(_response);
    }

    [HttpPost("register")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Register(
        [FromBody] RegistrationRequestDTO model)
    {
        ValidationResult validationResult =
            await _registrationValidator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            _response.StatusCode = HttpStatusCode.BadRequest;

            foreach (var error in validationResult.Errors)
            {
                _response.Errors.Add(error.ErrorMessage);
            }

            return BadRequest(_response);
        }

        bool isUserNameUnique =
            _userRepo.IsUniqueUser(model.UserName);

        if (!isUserNameUnique)
        {
            _response.StatusCode = HttpStatusCode.BadRequest;

            _response.Errors.Add(
                "User with the username already exists.");

            return BadRequest(_response);
        }

        
        model.Role = "clerk";

        var user = await _userRepo.Register(model);

        if (user == null)
        {
            _response.StatusCode = HttpStatusCode.BadRequest;

            _response.Errors.Add("Error while Registering");

            return BadRequest(_response);
        }

        _response.StatusCode = HttpStatusCode.OK;
        _response.Result = user;

        return Ok(_response);
    }
}