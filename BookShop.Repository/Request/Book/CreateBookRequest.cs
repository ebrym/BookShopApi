
using BookShop.Repository.Interfaces;
using BookShop.Repository.Response.Book;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Repository.Request.Book
{


    public class CreateBookRequest : IRequest<(bool Succeed, string Message, CreateBookResponse Response)>, IMapFrom<BookShop.Data.Book>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }
        public string ISBN { get; set; }
        public DateTimeOffset Published { get; set; }
        public string CategoryId { get; set; }
        public List<string> AuthorId { get; set; }


    }
}
