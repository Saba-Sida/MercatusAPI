using Domain.Entities;

namespace Application.Services.Auth;

public interface IJwtService
{
    string GenerateToken(User user);
}