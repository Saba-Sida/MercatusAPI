using Application.Models.DTOs;
using Application.Models.RequestModels;
using Application.Repositories.Users;
using Application.Security;
using Application.Validation;
using MercatusAPI.Models.RequestModels;

namespace Infrastructure.Validation;

public class UserAuthValidator : IUserAuthValidator
{
    private readonly IUsersRepository _usersRepository;
    private readonly IPasswordHasher _passwordHasher;

    public UserAuthValidator(IUsersRepository usersRepository,
        IPasswordHasher passwordHasher)
    {
        _usersRepository = usersRepository;
        _passwordHasher = passwordHasher;
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

    public async Task<(bool IsValid, string ErrorMessage)> VerifyLogInRequest(LogInRequest request)
    {
        (bool, string) isValid = (true, "");

        var passwordHash = await _usersRepository.GetUserPasswordHashByEmail(request.Email);

        if (passwordHash == null)
        {
            isValid = (false, "Email or Password is invalid.");
        }
        else
        {
            var passwordIsCorrect = _passwordHasher.VerifyPassword(request.Password, passwordHash);

            if (!passwordIsCorrect)
            {
                isValid = (false, "Email or Password is invalid.");
            }
        }

        return isValid;
    }
}