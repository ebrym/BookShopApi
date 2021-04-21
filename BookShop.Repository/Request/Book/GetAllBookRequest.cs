

using BookShop.Repository.Response.Book;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Repository.Request.Book
{
   
    public class GetAllBookRequest : IRequest<List<GetAllBookResponse>>
    {
      
    }

}
