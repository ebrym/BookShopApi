

using BookShop.Repository.Interfaces;
using BookShop.Repository.Request.Category;

namespace BookShop.Repository.Response.Category
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso >
    /// <Cref>
    /// Application.Interfaces.IMapFrom{Application.Request.Unit.UpdateUnitRequest}
    /// </Cref>
    /// </seealso>
    public class UpdateCategoryResponse : IMapFrom<UpdateCategoryRequest>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }

    }
}
