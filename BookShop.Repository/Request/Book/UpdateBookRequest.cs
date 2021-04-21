
using BookShop.Repository.Interfaces;
using BookShop.Repository.Response.Book;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Repository.Request.Book
{
    public class UpdateBookRequest : IRequest<(bool Succeed, string Message, UpdateBookResponse Response)>, IMapFrom<BookShop.Data.Book>
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public DateTimeOffset Published { get; set; }
        public string CategoryId { get; set; }

    }
}
