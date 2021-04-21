
using AutoMapper;
using BookShop.Data;
using BookShop.Repository.Interfaces;
using BookShop.Repository.Request.Book;
using BookShop.Repository.Response.Book;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace BookShop.Repository.Books.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateBookCommand : IRequestHandler<CreateBookRequest, (bool Succeed, string Message, CreateBookResponse Response)>
    {
        private readonly IApplicationDbContext applicationDb;
        private readonly IMapper mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationDb"></param>
        /// <param name="mapper"></param>
        public CreateBookCommand(IApplicationDbContext applicationDb, IMapper mapper)
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

        public async Task<(bool Succeed, string Message, CreateBookResponse Response)> Handle(CreateBookRequest request, CancellationToken cancellationToken)
        {

            var Book = mapper.Map<Book>(request);
            Book.Title = Book.Title.ToUpper();
            var BookExist = await applicationDb.Books.FirstOrDefaultAsync(x => x.ISBN.Equals(request.ISBN) || x.Title.ToUpper() == Book.Title, cancellationToken);
            if (BookExist != null)
                return (false, "The Book with this ISBN or TITLE has already been created", null);

          
            //perform insert 
            Book.DateCreated = DateTime.Now;

            var iBookAuthor = new List<BookAuthor>();


            foreach (var item in request.AuthorId)
            {
                var author = await applicationDb.Authors.FirstOrDefaultAsync(x => x.Id.Equals(item.ToString()), cancellationToken);

                if (author != null)
                {
                    var bookauthor = new BookAuthor
                    {
                        AuthorId = author.Id,
                        BookId = Book.Id
                    };
                    iBookAuthor.Add(bookauthor);
                }

            }

            Book.BookAuthors = iBookAuthor;

            await applicationDb.Books.AddAsync(Book, cancellationToken);

            await applicationDb.SaveChangesAsync(cancellationToken);

            // mapper can be used here
            var response = mapper.Map<CreateBookResponse>(request);
            // return response object 
            response.Id = Book.Id;
            return (true, "Book Created successfully", response);
        }
    }
}
