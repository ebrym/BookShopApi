using AutoMapper;
using BookShop.Data;
using BookShop.Repository.Interfaces;
using BookShop.Repository.Request.Category;
using BookShop.Repository.Response.Category;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Repository.Categorys.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateCategoryCommand : IRequestHandler<UpdateCategoryRequest, (bool Succeed, string Message, UpdateCategoryResponse Response)>
    {
        private readonly IApplicationDbContext applicationDb;
        private readonly IMapper mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationDb"></param>
        /// <param name="mapper"></param>
        public UpdateCategoryCommand(IApplicationDbContext applicationDb, IMapper mapper)
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

        public async Task<(bool Succeed, string Message, UpdateCategoryResponse Response)> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
        {

            var category = mapper.Map<Category>(request);

            var entity = await applicationDb.Categories.FindAsync(category.Id);

            /* var unitNameExist = await applicationDb.Categorys.FirstOrDefaultAsync(x => x.Name.Equals(request.Name), cancellationToken);
             if (unitNameExist != null)
                 return (false, "The Unit with this name has already been created", null);*/

            if (entity.Id == category.Id)
            {
                entity.Title = category.Title;
                await applicationDb.SaveChangesAsync(cancellationToken);
            }
            else
            {
                return (false, "The Category does not exist", null);
            }

            // mapper can be used here
            var response = mapper.Map<UpdateCategoryResponse>(request);
            response.Id = category.Id;
            // return response object 
            return (true, "Category Updated successfully", response);
        }
    }
}
