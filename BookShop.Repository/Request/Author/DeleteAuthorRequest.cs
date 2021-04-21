
using BookShop.Repository.Interfaces;
using BookShop.Repository.Response.Author;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Repository.Request.Author
{

 

    public class DeleteAuthorRequest : IRequest<(bool Succeed, string Message, DeleteAuthorResponse Response)>, IMapFrom<BookShop.Data.Author>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }


    }
}
