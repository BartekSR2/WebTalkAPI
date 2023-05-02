using AutoMapper;
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
        public IEnumerable<UserDto> FindUser(string name, string surname);
    }
    public class AccountService : IAccountService
    {
        private readonly WebTalkDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly ILogger<AccountService> _logger;
        private readonly IMapper _mapper;

        public AccountService(WebTalkDbContext dbContext,IPasswordHasher<User> passwordHasher,
            IJwtGenerator jwtGenerator, ILogger<AccountService> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _jwtGenerator = jwtGenerator;
            _logger = logger;
            _mapper = mapper;
        }

        public IEnumerable<UserDto> FindUser(string name, string surname)
        {
            
            var users = _dbContext.Users
                .Where(u =>
                u.Name.ToLower().Contains(name.ToLower()) ||
                u.Surname.ToLower().Contains(surname.ToLower()));

            if(users is null)
            {
                throw new NotFoundException($"cant find any user {name} {surname}");
            }

            var findedUsers = _mapper.Map<List<UserDto>>(users);

            return findedUsers;

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

            var token = _jwtGenerator.GetJwtToken(claims);

            _logger.LogInformation($"User {user.Email} login succesfull");
            return token;

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
