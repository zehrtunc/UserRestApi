using FluentValidation;
using UserManager.Models;

namespace UserManager.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Name)
                .NotEmpty().WithMessage("Name is required!")
                .Length(3, 10).WithMessage("Name must be at least 3 and no more than 10");

            RuleFor(user => user.Surname)
                .NotEmpty().WithMessage("Surname is required!")
                .Length(3, 10).WithMessage("Name must be at least 3 and no more than 10");

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email is reuired!")
                .EmailAddress().WithMessage("Invalid Email Address. Please enter a valid email address");

            RuleFor(employee => employee.Phone)
                .NotEmpty().WithMessage("Phone can not be empty.")
                .Matches(@"^\+90\d{10}$").WithMessage("Invalid Phone Number. Please enter a valid phone number");


        }
    }
}
