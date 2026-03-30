using Domain.Entities;

namespace Application.Repositories.Users;

public interface IUsersRepository
{
    Task<bool> EmailExistsInDb(string email);
    Task AddNewUser(User user);
}