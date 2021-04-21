
using BookShop.Repository.Interfaces;
using BookShop.Repository.Response.Author;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Repository.Request.Author
{
    public class UpdateAuthorRequest : IRequest<(bool Succeed, string Message, UpdateAuthorResponse Response)>, IMapFrom<BookShop.Data.Author>
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
