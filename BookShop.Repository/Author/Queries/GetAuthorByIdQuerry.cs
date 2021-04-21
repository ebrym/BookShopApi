
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookShop.Repository.Interfaces;
using BookShop.Repository.Request.Author;
using BookShop.Repository.Response.Author;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Repository.Authors.Queries
{
    /// <summary>
    /// 
    /// </summary>

    public class GetAuthorByIdQuerry : IRequestHandler<GetAuthorByIdRequest, GetAuthorByIdResponse>
    {
        private readonly IApplicationDbContext applicationDbContext;
        private readonly IMapper mapper;
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAuthorByIdQuerry"/> class.
        /// </summary>
        /// <param name="applicationDbContext">The application database context.</param>
        /// <param name="mapper">The mapper.</param>
        public GetAuthorByIdQuerry(IApplicationDbContext applicationDbContext, IMapper mapper)
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
        public async Task<GetAuthorByIdResponse> Handle(GetAuthorByIdRequest request, CancellationToken cancellationToken)
        {
            return await applicationDbContext.Authors.Where(x => x.Id == request.Id && x.IsDeleted == false).ProjectTo<GetAuthorByIdResponse>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }


    }
}
