
using AutoMapper;
using BookShop.Repository.Interfaces;
using BookShop.Repository.Request.Author;
using BookShop.Repository.Response.Author;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace BookShop.Repository.Authors.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteAuthorCommand : IRequestHandler<DeleteAuthorRequest, (bool Succeed, string Message, DeleteAuthorResponse Response)>
    {
        private readonly IApplicationDbContext applicationDb;
        private readonly IMapper mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationDb"></param>
        /// <param name="mapper"></param>
        public DeleteAuthorCommand(IApplicationDbContext applicationDb, IMapper mapper)
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

        public async Task<(bool Succeed, string Message, DeleteAuthorResponse Response)> Handle(DeleteAuthorRequest request, CancellationToken cancellationToken)
        {

            var Author = await applicationDb.Authors.Where(u => u.Id == request.Id).FirstOrDefaultAsync();

            Author.IsDeleted = true;
            applicationDb.Authors.Update(Author);

            int saved = await applicationDb.SaveChangesAsync(cancellationToken);

            // mapper can be used here
            var response = new DeleteAuthorResponse();
            // return response object 
            response.Id = Author.Id;

            if (saved > 0)
            {
                return (true, "Author Deleted successfully", response);
            }
            return (false, "There was a problem deleting the specified Author.", response);
        }
    }
}
