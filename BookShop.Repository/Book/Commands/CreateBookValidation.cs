
using BookShop.Repository.Request.Book;
using FluentValidation;

namespace BookShop.Repository.Books.Commands
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso>
    ///     <cref>FluentValidation.AbstractValidator{Application.Request.Unit.CreateUnitRequest}</cref>
    /// </seealso>
    public class CreateBookValidation : AbstractValidator<CreateBookRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateBookValidation"/> class.
        /// </summary>
        public CreateBookValidation()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(x => x.ISBN).NotEmpty().WithMessage("ISBN is required");

        }

    }
}
