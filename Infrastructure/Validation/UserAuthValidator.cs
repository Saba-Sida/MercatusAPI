using Application.Repositories.Users;
using Application.Validation;
using MercatusAPI.Models.RequestModels;

namespace Infrastructure.Validation;

public class UserAuthValidator : IUserAuthValidator
{
    private readonly IUsersRepository _usersRepository;

    public UserAuthValidator(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<(bool IsValid, string ErrorMessage)> ValidateSignUpRequest(SignUpRequest request)
    {
        (bool, string) isValid = (true, "");

        // TODO: 
        // Postponing some validations, refining better later
        if (await _usersRepository.EmailExistsInDb(request.Email))
            isValid = (false, "Such email is already registered.");

        return isValid;
    }
}