using auth.Application;
using auth.Domain;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace auth.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly SignupHandler _signupHandler;
    private readonly SessionHandler _sessionHandler;
    private readonly LoginHandler _loginHandler;
    private readonly ILogger<AuthController> _logger;

    public AuthController(SignupHandler signupHandler, SessionHandler sessionHandler, LoginHandler loginHandler, ILogger<AuthController> logger)
    {
        _signupHandler = signupHandler;
        _sessionHandler = sessionHandler;
        _loginHandler = loginHandler;
        _logger = logger;
    }

    [HttpPost("/signup")]
    public async Task<IActionResult> Signup([FromBody] UserLoginCredentials userCredentials)
    {
        await _signupHandler.CreateUser(userCredentials);
        return Ok();
    }

    [HttpPost("/login")]
    public async Task<IActionResult> Login([FromBody] UserLoginCredentials userCredentials)
    {
        var token = await _loginHandler.AuthenticateUser(userCredentials);

        if (!string.IsNullOrEmpty(token)) {
            return Ok(token);
        }
        else
        {
            return Unauthorized();
        }
    }

    [HttpGet("/sessions")]
    public async Task<IActionResult> ListSessions([FromQuery] string token)
    {
        if (TokenValidationService.ValidateToken(token, out string? validatedUserEmail))
        {
            return Ok(await _sessionHandler.ListSessions(validatedUserEmail!));
        }
        else
        {
            return Unauthorized();
        }
    }
}
