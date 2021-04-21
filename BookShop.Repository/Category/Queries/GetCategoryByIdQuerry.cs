
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookShop.Repository.Interfaces;
using BookShop.Repository.Request.Category;
using BookShop.Repository.Response.Category;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Repository.Categorys.Queries
{
    /// <summary>
    /// 
    /// </summary>

    public class GetCategoryByIdQuerry : IRequestHandler<GetCategoryByIdRequest, GetCategoryByIdResponse>
    {
        private readonly IApplicationDbContext applicationDbContext;
        private readonly IMapper mapper;
        /// <summary>
        /// Initializes a new instance of the <see cref="GetCategoryByIdQuerry"/> class.
        /// </summary>
        /// <param name="applicationDbContext">The application database context.</param>
        /// <param name="mapper">The mapper.</param>
        public GetCategoryByIdQuerry(IApplicationDbContext applicationDbContext, IMapper mapper)
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
        public async Task<GetCategoryByIdResponse> Handle(GetCategoryByIdRequest request, CancellationToken cancellationToken)
        {
            return await applicationDbContext.Categories.Where(x => x.Id == request.Id && x.IsDeleted == false).ProjectTo<GetCategoryByIdResponse>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }


    }
}
