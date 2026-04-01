using Application.ExtensionMethods;
using Application.Models.Common;
using Application.Models.DTOs;
using Application.Models.RequestModels;
using Application.Repositories.Users;
using Application.Security;
using Application.Services;
using Application.Services.Auth;
using Application.Services.Users;
using Application.Validation;
using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Services.Users;

public class UserService : IUserService
{
    private readonly IUserAuthValidator _userAuthValidator;
    private readonly ICacheService _cacheService;
    private readonly IEmailSender _emailSender;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUsersRepository _usersRepository;
    private readonly IJwtService _jwtService;

    private const int SignUpRequestTimeSpanInCacheStorageInMinutes = 5;

    public UserService(IUserAuthValidator userAuthValidator,
        ICacheService cacheService,
        IEmailSender emailSender,
        IPasswordHasher passwordHasher,
        IUsersRepository usersRepository,
        IJwtService jwtService)
    {
        _userAuthValidator = userAuthValidator;
        _cacheService = cacheService;
        _emailSender = emailSender;
        _passwordHasher = passwordHasher;
        _usersRepository = usersRepository;
        _jwtService = jwtService;
    }

    public async Task<OperationResponse<string>> SendSignUpRequest(SignUpRequest request)
    {
        var response = new OperationResponse<string>();

        var requestValidationResult = await _userAuthValidator.ValidateSignUpRequest(request);

        if (requestValidationResult.IsValid)
        {
            var useSignUpModel = ConvertSignUpRequestToSignUpReadyUserModel(request);
            await StoreUserSignUpModelInCache(useSignUpModel);

            string otp = GenerateOtp();
            await StoreOtpInCache(request.Email, otp);
            await SendOtpToEmail(request.Email, request.FirstName, otp);

            response.Data = "Check your email to verify with One-Time-Password";
        }
        else
        {
            response.IsSuccess = false;
            response.Status = OperationResponseStatus.ValidationError;
            response.ErrorMessage = requestValidationResult.ErrorMessage;
        }

        return response;
    }

    public async Task<OperationResponse> VerifyEmailByOtp(VerifyEmailRequest request)
    {
        var otpValidationResponse = new OperationResponse();

        var otpFromCache = await GetOtpFromCache(request.Email);

        if (otpFromCache == null)
        {
            otpValidationResponse
                .MakeGenerallyFailedResponse($"No OTP found or expired for {request.Email}");
        }
        else
        {
            if (otpFromCache != request.Otp)
            {
                otpValidationResponse
                    .MakeGenerallyFailedResponse("Wrong OTP for registration");
            }
            else
            {
                var userSignUpModel = await GetSignUpModelFromCache(request.Email);

                if (userSignUpModel == null)
                {
                    otpValidationResponse
                        .MakeGenerallyFailedResponse("Sign up process not found or expired");
                }
                else
                {
                    var userEntity = ConvertSignUpModelToUser(userSignUpModel);
                    await _usersRepository.AddNewUser(userEntity);
                }
            }
        }

        return otpValidationResponse;
    }

    /// <returns>JWT Token for user authentication/authorization</returns>
    public async Task<OperationResponse<string>> LogIn(LogInRequest request)
    {
        var jwtResponse = new OperationResponse<string>();

        var isCorrectUserLogInModel = await _userAuthValidator.VerifyLogInRequest(request);

        if (!isCorrectUserLogInModel.IsValid)
            jwtResponse.MakeGenerallyFailedResponse(isCorrectUserLogInModel.ErrorMessage);
        else
        {
            var user = await _usersRepository.GetUserByEmail(request.Email);

            if (user == null) jwtResponse.MakeGenerallyFailedResponse("User not found");
            else
            {
                string userJwt = _jwtService.GenerateToken(user);
                jwtResponse.Data = userJwt;
            }
        }

        return jwtResponse;
    }

    private async Task<UserSignUpModel?> GetSignUpModelFromCache(string email)
    {
        return await _cacheService.GetValueByKey<UserSignUpModel>(BuildMemoryCacheKeyForSignUp(email));
    }

    private async Task<string?> GetOtpFromCache(string email)
    {
        var signUpOtpCacheKey = BuildMemoryCacheKeyForSignUpOtp(email);
        return await _cacheService.GetValueByKey<string>(signUpOtpCacheKey);
    }

    private async Task StoreUserSignUpModelInCache(UserSignUpModel model)
    {
        var signUpCacheKey = BuildMemoryCacheKeyForSignUp(model.Email);
        await _cacheService.SetValueByKey(signUpCacheKey, model,
            TimeSpan.FromMinutes(SignUpRequestTimeSpanInCacheStorageInMinutes));
    }

    private async Task StoreOtpInCache(string email, string otp)
    {
        var signUpOtpCacheKey = BuildMemoryCacheKeyForSignUpOtp(email);
        await _cacheService.SetValueByKey(signUpOtpCacheKey, otp,
            TimeSpan.FromMinutes(SignUpRequestTimeSpanInCacheStorageInMinutes));
    }

    private async Task SendOtpToEmail(string email, string firstName, string otp)
    {
        await _emailSender.SendEmailAsync(
            email,
            "Registration OTP",
            $"Hello, Dear {firstName}, your OTP is: {otp}");
    }

    private UserSignUpModel ConvertSignUpRequestToSignUpReadyUserModel(SignUpRequest request)
    {
        return new UserSignUpModel
        (
            request.FirstName,
            request.LastName,
            request.Email,
            _passwordHasher.HashPassword(request.Password)
        );
    }

    private User ConvertSignUpModelToUser(UserSignUpModel model)
    {
        return new User
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Created = DateTime.Now,
            Email = model.Email,
            PasswordHash = model.PasswordHash,
            Role = RolesEnum.User
        };
    }

    private static string GenerateOtp()
    {
        return new Random().Next(10000, 100000).ToString("D5");
    }

    private static string BuildMemoryCacheKeyForSignUp(string email)
    {
        return $"sign_up_key_for_{email}";
    }

    private static string BuildMemoryCacheKeyForSignUpOtp(string email)
    {
        return $"sign_up_otp_key_for_{email}";
    }
}