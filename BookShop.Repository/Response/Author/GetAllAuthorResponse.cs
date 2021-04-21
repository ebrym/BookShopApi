

using BookShop.Repository.Interfaces;

namespace BookShop.Repository.Response.Author
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso >
    /// <cref>
    /// Application.Interfaces.IMapFrom{Domain.Entities.Author}
    /// </cref>
    /// </seealso>
    public class GetAllAuthorResponse : IMapFrom<BookShop.Data.Author>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets the FirstName.
        /// </summary>
        /// <value>
        /// The FirstName.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the LastName.
        /// </summary>
        /// <value>
        /// The LastName.
        /// </value>
        public string LastName { get; set; }



    }
}
