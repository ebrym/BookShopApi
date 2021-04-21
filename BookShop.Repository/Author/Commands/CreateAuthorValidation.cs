
using BookShop.Repository.Request.Author;
using FluentValidation;

namespace BookShop.Repository.Authors.Commands
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso>
    ///     <cref>FluentValidation.AbstractValidator{Application.Request.Unit.CreateUnitRequest}</cref>
    /// </seealso>
    public class CreateAuthorValidation : AbstractValidator<CreateAuthorRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAuthorValidation"/> class.
        /// </summary>
        public CreateAuthorValidation()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name is required");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name is required");

        }

    }
}
