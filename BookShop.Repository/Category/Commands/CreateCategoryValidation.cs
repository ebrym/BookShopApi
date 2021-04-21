
using BookShop.Repository.Request.Category;
using FluentValidation;

namespace BookShop.Repository.Categorys.Commands
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso>
    ///     <cref>FluentValidation.AbstractValidator{Application.Request.Unit.CreateUnitRequest}</cref>
    /// </seealso>
    public class CreateCategoryValidation : AbstractValidator<CreateCategoryRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCategoryValidation"/> class.
        /// </summary>
        public CreateCategoryValidation()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
        }

    }
}
