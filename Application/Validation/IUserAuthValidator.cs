using Application.Models.DTOs;
using Application.Models.RequestModels;

namespace Application.Validation;

public interface IUserAuthValidator
{
    Task<(bool IsValid, string ErrorMessage)> ValidateSignUpRequest(SignUpRequest request);
    Task<(bool IsValid, string ErrorMessage)> VerifyLogInRequest(LogInRequest request);
}