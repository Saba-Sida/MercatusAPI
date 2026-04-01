using System.ComponentModel.DataAnnotations;

namespace Application.Models.RequestModels;

public record SignUpRequest(
    [Required] string FirstName,
    [Required] string LastName,
    [Required, EmailAddress] string Email,
    [Required, MinLength(6)] string Password,
    [Required, MinLength(6)] string PasswordConfirm
    );