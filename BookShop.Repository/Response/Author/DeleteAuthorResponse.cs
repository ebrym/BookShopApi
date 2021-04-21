
using BookShop.Repository.Interfaces;
using BookShop.Repository.Request.Author;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Repository.Response.Author
{
   
    /// <summary>
    /// 
    /// </summary>
    public class DeleteAuthorResponse: IMapFrom<CreateAuthorRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
       


    }
}
