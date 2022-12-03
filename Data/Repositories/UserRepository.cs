using ArmoryManagerApi.Helper;
using ArmoryManagerApi.Interfaces;
using ArmoryManagerApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace ArmoryManagerApi.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ArmoryManagerContext _context;

    public UserRepository(ArmoryManagerContext context)
    {
        _context = context;
    }

    public async Task<User> Authenticate(string username, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);

        if(user == null || user.Salt == null)
        {
            return null!;
        }

        if(!MatchPasswordHash(password, user.Password, user.Salt))
        {
            return null!;
        }

        return user;
    }

    public void Register(string username, string password)
    {
        byte[] passwordHash, salt;

        using var hmac = new HMACSHA512();
        salt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        var user = new User()
        {
            UserName = username,
            Password = passwordHash,
            Salt = salt,
            CreatedAt = DateTime.Now.ToString(Constants.DATE_TIME_FORMAT),
            UpdatedAt = DateTime.Now.ToString(Constants.DATE_TIME_FORMAT)
        };

        _context.Users.Add(user);
    }

    public Task<bool> UserAlreadyExists(string username)
    {
        return _context.Users.AnyAsync(x => x.UserName == username);
    }

    private static bool MatchPasswordHash(string passwordText, byte[] password, byte[] salt)
    {
        using var hmac = new HMACSHA512(salt);

        var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(passwordText));

        return password.SequenceEqual(passwordHash);
    }
}
