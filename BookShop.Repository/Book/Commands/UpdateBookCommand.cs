using AutoMapper;
using BookShop.Data;
using BookShop.Repository.Interfaces;
using BookShop.Repository.Request.Book;
using BookShop.Repository.Response.Book;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Repository.Books.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateBookCommand : IRequestHandler<UpdateBookRequest, (bool Succeed, string Message, UpdateBookResponse Response)>
    {
        private readonly IApplicationDbContext applicationDb;
        private readonly IMapper mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationDb"></param>
        /// <param name="mapper"></param>
        public UpdateBookCommand(IApplicationDbContext applicationDb, IMapper mapper)
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

        public async Task<(bool Succeed, string Message, UpdateBookResponse Response)> Handle(UpdateBookRequest request, CancellationToken cancellationToken)
        {

            var unit = mapper.Map<Book>(request);

            var entity = await applicationDb.Books.FindAsync(unit.Id);

            var category = await applicationDb.Categories.FindAsync(request.CategoryId);
            /* var unitNameExist = await applicationDb.Books.FirstOrDefaultAsync(x => x.Name.Equals(request.Name), cancellationToken);
             if (unitNameExist != null)
                 return (false, "The Unit with this name has already been created", null);*/

            if (entity.Id == unit.Id)
            {
                entity.Title = unit.Title;
                entity.ISBN = unit.ISBN;
                entity.Published = unit.Published;
                entity.Category = category;


                await applicationDb.SaveChangesAsync(cancellationToken);
            }
            else
            {
                return (false, "The Book does not exist", null);
            }

            // mapper can be used here
            var response = mapper.Map<UpdateBookResponse>(request);
            response.Id = unit.Id;
            // return response object 
            return (true, "Book Updated successfully", response);
        }
    }
}
