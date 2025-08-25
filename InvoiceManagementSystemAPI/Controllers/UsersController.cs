using System.Net;
using InvoiceManagementSystemAPI.Models;
using InvoiceManagementSystemAPI.Models.Dto;
using InvoiceManagementSystemAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManagementSystemAPI.Controllers;
[Route("api/UsersAuth")]
[ApiController]
public class UsersController:ControllerBase
{
    protected APIResponse _response;
    private readonly IUserRepository _userRepo;
    public UsersController(IUserRepository userRepo)
    {
        _userRepo = userRepo;
        this._response = new();
    }

    [HttpPost("login")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
    {
        var loginResponse = await _userRepo.Login(model);
        if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
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
    public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO model)
    {
        bool ifUserNameUnique = _userRepo.IsUniqueUser(model.UserName);
        if (!ifUserNameUnique)
        {
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.Errors.Add("User with the username already exists ");
            return BadRequest(_response);
            }

        var user = await _userRepo.Register(model);
        if (user == null)
        {
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.Errors.Add("Error while Registering");
            return BadRequest(_response);
        }

        _response.StatusCode = HttpStatusCode.OK;
        return Ok(_response);
    }
}