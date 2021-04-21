

using BookShop.Repository.Interfaces;

namespace BookShop.Repository.Response.Category
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso >
    /// <cref>
    /// Application.Interfaces.IMapFrom{Domain.Entities.Author}
    /// </cref>
    /// </seealso>
    public class GetAllCategoryResponse : IMapFrom<BookShop.Data.Category>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets the Title.
        /// </summary>
        /// <value>
        /// The Title.
        /// </value>
        public string Title { get; set; }



    }
}
