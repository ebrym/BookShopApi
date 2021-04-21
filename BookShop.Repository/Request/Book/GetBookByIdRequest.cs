

using BookShop.Repository.Response.Book;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Repository.Request.Book
{
    /// <summary>
    /// 
    /// </summary>
  
    public class GetBookByIdRequest : IRequest<GetBookByIdResponse>
    {
        public string Id { get; set; }
    }

    public class GetBookByCategoryIdRequest : IRequest<List<GetBookByIdResponse>>
    {
        public string CategoryId { get; set; }
    }
    public class GetBookByAuthorIdRequest : IRequest<List<GetBookByIdResponse>>
    {
        public string AuthorId { get; set; }
    }
}
