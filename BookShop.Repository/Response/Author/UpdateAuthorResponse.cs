

using BookShop.Repository.Interfaces;
using BookShop.Repository.Request.Author;

namespace BookShop.Repository.Response.Author
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso >
    /// <Cref>
    /// Application.Interfaces.IMapFrom{Application.Request.Unit.UpdateUnitRequest}
    /// </Cref>
    /// </seealso>
    public class UpdateAuthorResponse : IMapFrom<UpdateAuthorRequest>
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
