

using BookShop.Repository.Response.Author;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Repository.Request.Author
{
   
    public class GetAllAuthorRequest : IRequest<List<GetAllAuthorResponse>>
    {
      
    }

}
