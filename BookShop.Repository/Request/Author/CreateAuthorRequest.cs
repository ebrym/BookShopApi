
using BookShop.Repository.Interfaces;
using BookShop.Repository.Response.Author;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Repository.Request.Author
{


    public class CreateAuthorRequest : IRequest<(bool Succeed, string Message, CreateAuthorResponse Response)>, IMapFrom<BookShop.Data.Author>
    {
        /// <summary>
        /// 
        /// </summary>
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> BookId { get; set; } 

    }
}
