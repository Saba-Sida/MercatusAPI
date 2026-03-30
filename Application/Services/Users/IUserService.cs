using Application.Models.Common;
using Application.Models.RequestModels;
using MercatusAPI.Models.RequestModels;

namespace Application.Services.Users;

public interface IUserService
{
    Task<OperationResponse<string>> SendSignUpRequest(SignUpRequest request);
    Task<OperationResponse> VerifyEmailByOtp(VerifyEmailRequest request);
}