using Domain.Enums;

namespace Application.Models.DTOs;

public record UserSignUpModel(
    string FirstName,
    string LastName,
    string Email,
    string PasswordHash,
    RolesEnum Role
    );