
using BookShop.Repository.Interfaces;
using BookShop.Repository.Request.Category;
using BookShop.Repository.Response.Category;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Repository.Categorys.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllCategoryQuery : IRequestHandler<GetAllCategoryRequest, List<GetAllCategoryResponse>>
    {
        private readonly IApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllCategoryQuery"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public GetAllCategoryQuery(IApplicationDbContext context)
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
        public async Task<List<GetAllCategoryResponse>> Handle(GetAllCategoryRequest request, CancellationToken cancellationToken)
        {
            return await context.Categories.Where(u => u.IsDeleted == false).Select(a => new GetAllCategoryResponse
            {
                Title = a.Title,
                Id = a.Id
            }).ToListAsync(cancellationToken);

        }


    }
}
