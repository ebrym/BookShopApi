

using BookShop.Repository.Response.Author;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Repository.Request.Author
{
    /// <summary>
    /// 
    /// </summary>
  
    public class GetAuthorByIdRequest : IRequest<GetAuthorByIdResponse>
    {
        public string Id { get; set; }
    }
}
