
using AutoMapper;
using BookShop.Data;
using BookShop.Repository.Interfaces;
using BookShop.Repository.Request.Author;
using BookShop.Repository.Response.Author;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace BookShop.Repository.Authors.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateAuthorCommand : IRequestHandler<CreateAuthorRequest, (bool Succeed, string Message, CreateAuthorResponse Response)>
    {
        private readonly IApplicationDbContext applicationDb;
        private readonly IMapper mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationDb"></param>
        /// <param name="mapper"></param>
        public CreateAuthorCommand(IApplicationDbContext applicationDb, IMapper mapper)
        {
            this.applicationDb = applicationDb;
            this.mapper = mapper;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>

        public async Task<(bool Succeed, string Message, CreateAuthorResponse Response)> Handle(CreateAuthorRequest request, CancellationToken cancellationToken)
        {

            var author = mapper.Map<Author>(request);
            author.FirstName = author.FirstName.ToUpper();
            //var authorExist = await applicationDb.Authors.FirstOrDefaultAsync(x => x.FirstName.Equals(request.FirstName), cancellationToken);
            //if (authorExist != null)
            //    return (false, "The Author with this name has already been created", null);
            //perform insert 
            author.DateCreated = DateTime.Now;
            await applicationDb.Authors.AddAsync(author, cancellationToken);

            await applicationDb.SaveChangesAsync(cancellationToken);

            // mapper can be used here
            var response = mapper.Map<CreateAuthorResponse>(request);
            // return response object 
            response.Id = author.Id;
            return (true, "Author Created successfully", response);
        }
    }
}
