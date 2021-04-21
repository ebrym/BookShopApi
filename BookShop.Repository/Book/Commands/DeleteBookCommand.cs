
using AutoMapper;
using BookShop.Repository.Interfaces;
using BookShop.Repository.Request.Book;
using BookShop.Repository.Response.Book;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace BookShop.Repository.Books.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteBookCommand : IRequestHandler<DeleteBookRequest, (bool Succeed, string Message, DeleteBookResponse Response)>
    {
        private readonly IApplicationDbContext applicationDb;
        private readonly IMapper mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationDb"></param>
        /// <param name="mapper"></param>
        public DeleteBookCommand(IApplicationDbContext applicationDb, IMapper mapper)
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

        public async Task<(bool Succeed, string Message, DeleteBookResponse Response)> Handle(DeleteBookRequest request, CancellationToken cancellationToken)
        {

            var Book = await applicationDb.Books.Where(u => u.Id == request.Id).FirstOrDefaultAsync();

            Book.IsDeleted = true;
            applicationDb.Books.Update(Book);

            int saved = await applicationDb.SaveChangesAsync(cancellationToken);

            // mapper can be used here
            var response = new DeleteBookResponse();
            // return response object 
            response.Id = Book.Id;

            if (saved > 0)
            {
                return (true, "Book Deleted successfully", response);
            }
            return (false, "There was a problem deleting the specified Book.", response);
        }
    }
}
