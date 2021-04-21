

using BookShop.Repository.Response.Category;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Repository.Request.Category
{
   
    public class GetAllCategoryRequest : IRequest<List<GetAllCategoryResponse>>
    {
      
    }

}
