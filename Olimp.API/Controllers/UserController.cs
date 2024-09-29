using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Olimp.API.DTOs;
using Olimp.UserManagement.Application.Interfaces;

namespace Olimp.API.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController(IAuthenticationService authenticationService) : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService = authenticationService;

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDTO registerDTO, CancellationToken ct)
        {
            Result<bool, List<string>> resultRegister = await _authenticationService.RegisterAsync(
                registerDTO.FirstName,
                registerDTO.LastName,
                registerDTO.Email,
                registerDTO.PhoneNumber,
                registerDTO.PostalCode,
                registerDTO.Region,
                registerDTO.City,
                registerDTO.Street,
                registerDTO.HouseNumber,
                registerDTO.ApartmentNumber,
                registerDTO.Password,
                registerDTO.RepeatPassword,
                ct);

            if (resultRegister.IsFailure)
                return BadRequest(string.Join("\n", resultRegister.Error));

            return Ok("Registration was successful");
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDTO loginDTO, CancellationToken ct)
        {
            Result<string> resultLogin = await _authenticationService.LoginAsync(
                loginDTO.Email,
                loginDTO.Password,
                ct);

            if (resultLogin.IsFailure)
                return BadRequest(resultLogin.Error);

            HttpContext.Response.Cookies.Append("cookies", resultLogin.Value);

            return Ok("Successful login");
        }
    }
}
