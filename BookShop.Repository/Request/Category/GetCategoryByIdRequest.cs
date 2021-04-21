

using BookShop.Repository.Response.Category;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Repository.Request.Category
{
    /// <summary>
    /// 
    /// </summary>
  
    public class GetCategoryByIdRequest : IRequest<GetCategoryByIdResponse>
    {
        public string Id { get; set; }
    }
}
