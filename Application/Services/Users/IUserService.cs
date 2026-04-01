using Application.Models.Common;
using Application.Models.RequestModels;

namespace Application.Services.Users;

public interface IUserService
{
    Task<OperationResponse<string>> SendSignUpRequest(SignUpRequest request);
    Task<OperationResponse> VerifyEmailByOtp(VerifyEmailRequest request);
    
    /// <returns>JWT Token for user authentication/authorization</returns>
    Task<OperationResponse<string>> LogIn(LogInRequest request);
}