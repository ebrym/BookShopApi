
using BookShop.Repository.Interfaces;
using System;
using System.Collections.Generic;

namespace BookShop.Repository.Response.Book
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso>
    /// <cref>
    /// Application.Interfaces.IMapFrom{Domain.Entities.Book
    /// </cref>
    /// </seealso>
    public class GetBookByIdResponse : IMapFrom<BookShop.Data.Book>
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

        /// <summary>
        /// Gets or sets the ISBN.
        /// </summary>
        /// <value>
        /// The ISBN.
        /// </value>
        public string ISBN { get; set; }
        /// <summary>
        /// Gets or sets the Published date.
        /// </summary>
        /// <value>
        /// The Published date.
        /// </value>
        public DateTimeOffset Published { get; set; }
        /// <summary>
        /// Gets or sets the Category.
        /// </summary>
        /// <value>
        /// The Category.
        /// </value>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the authors.
        /// </summary>
        /// <value>
        /// The authors.
        /// </value>
        public List<string> Author { get; set; }


    }
}
