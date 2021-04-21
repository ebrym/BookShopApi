
using BookShop.Repository.Interfaces;
using BookShop.Repository.Request.Category;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Repository.Response.Category
{
   
    /// <summary>
    /// 
    /// </summary>
    public class DeleteCategoryResponse : IMapFrom<CreateCategoryRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
       


    }
}
