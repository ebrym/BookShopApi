
using AutoMapper;
using AutoMapper.QueryableExtensions;
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

    public class GetBookByCategoryIdQuery : IRequestHandler<GetBookByCategoryIdRequest, List<GetBookByIdResponse>>
    {
        private readonly IApplicationDbContext applicationDbContext;
        private readonly IMapper mapper;
        /// <summary>
        /// Initializes a new instance of the <see cref="GetBookByIdQuerry"/> class.
        /// </summary>
        /// <param name="applicationDbContext">The application database context.</param>
        /// <param name="mapper">The mapper.</param>
        public GetBookByCategoryIdQuery(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            this.applicationDbContext = applicationDbContext;
            this.mapper = mapper;
        }

        /// <summary>
        /// Handles a request
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        /// Response from the request
        /// </returns>
        public async Task<List<GetBookByIdResponse>> Handle(GetBookByCategoryIdRequest request, CancellationToken cancellationToken)
        {

            var query = await applicationDbContext.Books
                  .Where(x => x.CategoryId == request.CategoryId && x.IsDeleted == false)
                  .Select(a => new GetBookByIdResponse
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
                var author = await applicationDbContext.BookAuthors
                   .Include(x => x.Author)
                   .Where(x => x.BookId == item.Id)
                   .Select(a => new  { v = $"{a.Author.FirstName} {a.Author.LastName}" }).ToListAsync();
                iBookAuthor.AddRange(author.Select(a=>a.v));
                item.Author = iBookAuthor;
            }
            return query;
        }


    }
}
