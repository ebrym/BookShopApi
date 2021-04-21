
using AutoMapper;
using BookShop.Data;
using BookShop.Repository.Interfaces;
using BookShop.Repository.Request.Category;
using BookShop.Repository.Response.Category;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace BookShop.Repository.Categorys.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateCategoryCommand : IRequestHandler<CreateCategoryRequest, (bool Succeed, string Message, CreateCategoryResponse Response)>
    {
        private readonly IApplicationDbContext applicationDb;
        private readonly IMapper mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationDb"></param>
        /// <param name="mapper"></param>
        public CreateCategoryCommand(IApplicationDbContext applicationDb, IMapper mapper)
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

        public async Task<(bool Succeed, string Message, CreateCategoryResponse Response)> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
        {

            var Category = mapper.Map<Category>(request);
            Category.Title = Category.Title.ToUpper();
            var CategoryExist = await applicationDb.Categories.FirstOrDefaultAsync(x => x.Title.Equals(request.Title), cancellationToken);
            if (CategoryExist != null)
                return (false, "The Category with this Title has already been created", null);
            //perform insert 
            Category.DateCreated = DateTime.Now;
            await applicationDb.Categories.AddAsync(Category, cancellationToken);

            await applicationDb.SaveChangesAsync(cancellationToken);

            // mapper can be used here
            var response = mapper.Map<CreateCategoryResponse>(request);
            // return response object 
            response.Id = Category.Id;
            return (true, "Category Created successfully", response);
        }
    }
}
