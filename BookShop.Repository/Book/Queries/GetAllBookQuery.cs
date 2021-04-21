
using BookShop.Repository.Interfaces;
using BookShop.Repository.Request.Book;
using BookShop.Repository.Response.Book;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Repository.Books.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllBookQuery : IRequestHandler<GetAllBookRequest, List<GetAllBookResponse>>
    {
        private readonly IApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllBookQuery"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public GetAllBookQuery(IApplicationDbContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// Handles a request
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        /// Response from the request
        /// </returns>
        public async Task<List<GetAllBookResponse>> Handle(GetAllBookRequest request, CancellationToken cancellationToken)
        {
            var query = await context.Books
                  .Where(x => x.IsDeleted == false)
                  .Select(a => new GetAllBookResponse
                  {
                      Title = a.Title,
                      ISBN = a.ISBN,
                      Published = a.Published,
                      Category = a.Category.Title,
                      Id = a.Id,
                  }).ToListAsync(cancellationToken);





            foreach (var item in query)
            {
                var iBookAuthor = new List<string>();
                var author = await context.BookAuthors
                   .Include(x => x.Author)
                   .Where(x => x.BookId == item.Id)
                   .Select(a => new { v = $"{a.Author.FirstName} {a.Author.LastName}" }).ToListAsync();
                iBookAuthor.AddRange(author.Select(a => a.v));
                item.Author = iBookAuthor;
            }
            return query;

        }


    }
}
