using System.ComponentModel.DataAnnotations;

namespace Application.Models.RequestModels;

public record LogInRequest(
    [Required, EmailAddress] string Email,
    [Required] string Password);