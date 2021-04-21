
using BookShop.Repository.Interfaces;
using BookShop.Repository.Response.Book;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Repository.Request.Book
{

 

    public class DeleteBookRequest : IRequest<(bool Succeed, string Message, DeleteBookResponse Response)>, IMapFrom<BookShop.Data.Book>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }


    }
}
