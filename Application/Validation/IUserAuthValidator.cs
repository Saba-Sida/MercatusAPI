using MercatusAPI.Models.RequestModels;

namespace Application.Validation;

public interface IUserAuthValidator
{
    Task<(bool IsValid, string ErrorMessage)> ValidateSignUpRequest(SignUpRequest request);
}