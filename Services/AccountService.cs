using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebTalkApi.Entities;
using WebTalkApi.Exceptions;
using WebTalkApi.Models;

namespace WebTalkApi.Services
{
    public interface IAccountService
    {
        public void RegisterUser(RegisterUserDto registerDto);
        public string LoginUser(LoginUserDto  loginDto);
    }
    public class AccountService : IAccountService
    {
        private readonly WebTalkDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly ILogger<AccountService> _logger;

        public AccountService(WebTalkDbContext dbContext,IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings, ILogger<AccountService> logger)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
            _logger = logger;
        }
        public string LoginUser(LoginUserDto loginDto)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == loginDto.Email);

            if(user is null)
            {
                throw new BadRequestException("Incorrect Email or Password");
            }

            if(_passwordHasher.VerifyHashedPassword(user, user.HashedPassword, loginDto.Password)
                == PasswordVerificationResult.Failed)
            {
                _logger.LogWarning($"User {user.Email} login failed. Bad password");
                throw new BadRequestException("Incorrect Email or Password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Surname, string.IsNullOrEmpty(user.Surname) ? "x": user.Surname ),

            };

            var jwtKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(jwtKey, SecurityAlgorithms.HmacSha256);

            var expirationDate = DateTime.Now.AddDays(_authenticationSettings.ExpireDays);


            var jwtToken = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expirationDate,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();

            _logger.LogInformation($"User {user.Email} login succesfull");
            return tokenHandler.WriteToken(jwtToken);

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
            _logger.LogInformation($"Create new user {newCreatedUser.Email}, {newCreatedUser.Name} {newCreatedUser.Surname}");

        }
    }
}
