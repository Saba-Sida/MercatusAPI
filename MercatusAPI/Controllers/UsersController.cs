using Application.Models.Common;
using Application.Models.RequestModels;
using Application.Services.Users;
using MercatusAPI.ExternalMethods;
using MercatusAPI.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace MercatusAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController: ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost]
    [Route("sign-up")]
    public async Task<IActionResult> SignUpNewUser([FromBody] SignUpRequest request)
    {
        OperationResponse<string> response = await _userService.SendSignUpRequest(request);

        return this.FormatResponse(response);
    }

    [HttpPost]
    [Route("verify-email")]
    public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailRequest request)
    {
        OperationResponse response = await _userService.VerifyEmailByOtp(request);

        return this.FormatResponse(response);
    }

    /// <returns>If successful: JWT token for user</returns>
    [HttpPost]
    [Route("log-in")]
    public async Task<IActionResult> LogIn([FromBody] LogInRequest request)
    {
        OperationResponse<string> response = await _userService.LogIn(request);

        return this.FormatResponse(response);
    }
}