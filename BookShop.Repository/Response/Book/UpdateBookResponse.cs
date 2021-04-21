

using BookShop.Repository.Interfaces;
using BookShop.Repository.Request.Book;

namespace BookShop.Repository.Response.Book
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso >
    /// <Cref>
    /// Application.Interfaces.IMapFrom{Application.Request.Unit.UpdateUnitRequest}
    /// </Cref>
    /// </seealso>
    public class UpdateBookResponse : IMapFrom<UpdateBookRequest>
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
