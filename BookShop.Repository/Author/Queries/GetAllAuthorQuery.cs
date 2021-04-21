
using BookShop.Repository.Interfaces;
using BookShop.Repository.Request.Author;
using BookShop.Repository.Response.Author;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Repository.Authors.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllAuthorQuery : IRequestHandler<GetAllAuthorRequest, List<GetAllAuthorResponse>>
    {
        private readonly IApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllAuthorQuery"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public GetAllAuthorQuery(IApplicationDbContext context)
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
        public async Task<List<GetAllAuthorResponse>> Handle(GetAllAuthorRequest request, CancellationToken cancellationToken)
        {
            return await context.Authors.Where(u => u.IsDeleted == false).Select(a => new GetAllAuthorResponse
            {
                FirstName = a.FirstName,
                LastName = a.LastName,
                Id = a.Id
            }).ToListAsync(cancellationToken);

        }


    }
}
