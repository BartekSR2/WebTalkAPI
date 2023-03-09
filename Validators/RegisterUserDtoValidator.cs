using FluentValidation;
using WebTalkApi.Entities;
using WebTalkApi.Models;

namespace WebTalkApi.Validators
{
    public class RegisterUserDtoValidator: AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(WebTalkDbContext dbContext)
        {

            RuleFor(u => u.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(u => u.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Users.Any(u => u.Email == value);
                    if (emailInUse)
                    {
                        context.AddFailure("Email", "Email is already in use");
                    }
                });
            RuleFor(u => u.Name).NotEmpty();

            RuleFor(u => u.Password).NotEmpty().MinimumLength(5);
            RuleFor(u => u.ConfirmPassword).NotEmpty().Equal(u => u.Password);
        }

        
    }
}
