using AutoMapper;
using BookShop.Data;
using BookShop.Repository.Interfaces;
using BookShop.Repository.Request.Author;
using BookShop.Repository.Response.Author;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Repository.Authors.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateAuthorCommand : IRequestHandler<UpdateAuthorRequest, (bool Succeed, string Message, UpdateAuthorResponse Response)>
    {
        private readonly IApplicationDbContext applicationDb;
        private readonly IMapper mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationDb"></param>
        /// <param name="mapper"></param>
        public UpdateAuthorCommand(IApplicationDbContext applicationDb, IMapper mapper)
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

        public async Task<(bool Succeed, string Message, UpdateAuthorResponse Response)> Handle(UpdateAuthorRequest request, CancellationToken cancellationToken)
        {

            var unit = mapper.Map<Author>(request);

            var entity = await applicationDb.Authors.FindAsync(unit.Id);

            /* var unitNameExist = await applicationDb.Authors.FirstOrDefaultAsync(x => x.Name.Equals(request.Name), cancellationToken);
             if (unitNameExist != null)
                 return (false, "The Unit with this name has already been created", null);*/

            if (entity.Id == unit.Id)
            {
                entity.FirstName = unit.FirstName;
                entity.LastName = unit.LastName;
                await applicationDb.SaveChangesAsync(cancellationToken);
            }
            else
            {
                return (false, "The Author does not exist", null);
            }

            // mapper can be used here
            var response = mapper.Map<UpdateAuthorResponse>(request);
            response.Id = unit.Id;
            // return response object 
            return (true, "Author Updated successfully", response);
        }
    }
}
