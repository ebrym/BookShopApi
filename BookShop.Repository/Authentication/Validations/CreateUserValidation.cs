
using BookShop.Repository.Request.User;
using FluentValidation;
using FluentValidation.Validators;

namespace BookShop.Repository.Authentication.Validations
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso>
    ///     <cref>FluentValidation.AbstractValidator{Application.Request.User.CreateUserRequest}</cref>
    /// </seealso>
    public class CreateUserValidation : AbstractValidator<CreateUserRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserValidation"/> class.
        /// </summary>
        public CreateUserValidation()
        {
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
            RuleFor(x => x.Email).EmailAddress(mode: EmailValidationMode.AspNetCoreCompatible);
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.FullName).NotEmpty();
            RuleFor(x => x.Roles).Must(x => x.Length > 0).WithMessage("At least one role must be provided");
         
        }
    }
}