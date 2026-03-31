using Application.Models.DTOs;
using Application.Repositories.Users;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Users;

public class UsersRepository: IUsersRepository
{
    private readonly AppDbContext _dbContext;

    public UsersRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<bool> EmailExistsInDb(string email)
    {
        return await _dbContext.Users.AnyAsync(user => user.Email == email);
    }

    public async Task AddNewUser(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<User?> GetUserByEmail(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);
    }

    public async Task<string?> GetUserPasswordHashByEmail(string email)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .Where(user => user.Email == email)
            .Select(user => user.PasswordHash)
            .FirstOrDefaultAsync();
    }
}