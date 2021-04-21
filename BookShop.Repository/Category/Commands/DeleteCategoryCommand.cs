
using AutoMapper;
using BookShop.Repository.Interfaces;
using BookShop.Repository.Request.Category;
using BookShop.Repository.Response.Category;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace BookShop.Repository.Categorys.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteCategoryCommand : IRequestHandler<DeleteCategoryRequest, (bool Succeed, string Message, DeleteCategoryResponse Response)>
    {
        private readonly IApplicationDbContext applicationDb;
        private readonly IMapper mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationDb"></param>
        /// <param name="mapper"></param>
        public DeleteCategoryCommand(IApplicationDbContext applicationDb, IMapper mapper)
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

        public async Task<(bool Succeed, string Message, DeleteCategoryResponse Response)> Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
        {

            var Category = await applicationDb.Categories.Where(u => u.Id == request.Id).FirstOrDefaultAsync();

            Category.IsDeleted = true;
            applicationDb.Categories.Update(Category);

            int saved = await applicationDb.SaveChangesAsync(cancellationToken);

            // mapper can be used here
            var response = new DeleteCategoryResponse();
            // return response object 
            response.Id = Category.Id;

            if (saved > 0)
            {
                return (true, "Category Deleted successfully", response);
            }
            return (false, "There was a problem deleting the specified Category.", response);
        }
    }
}
