using System.ComponentModel.DataAnnotations;

namespace Application.Models.RequestModels;

public record VerifyEmailRequest(
    [Required] string Email,
    [Required] string Otp
    );