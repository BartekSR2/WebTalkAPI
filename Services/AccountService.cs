using Microsoft.AspNetCore.Identity;
using WebTalkApi.Entities;
using WebTalkApi.Models;

namespace WebTalkApi.Services
{
    public interface IAccountService
    {
        public void RegisterUser(RegisterUserDto registerDto);
        public void LoginUser(LoginUserDto  loginDto);
    }
    public class AccountService : IAccountService
    {
        private readonly WebTalkDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        public AccountService(WebTalkDbContext dbContext,IPasswordHasher<User> passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }
        public void LoginUser(LoginUserDto loginDto)
        {
            throw new NotImplementedException();
        }

        public void RegisterUser(RegisterUserDto registerDto)
        {
            var newCreatedUser = new User()
            {
                Email = registerDto.Email,
                Name = registerDto.Name,
                Surname = string.IsNullOrEmpty(registerDto.Surname) ? null : registerDto.Surname,
                
            };

            newCreatedUser.HashedPassword = _passwordHasher.HashPassword(newCreatedUser, registerDto.Password);

            _dbContext.Users.Add(newCreatedUser);
            _dbContext.SaveChanges();

        }
    }
}
