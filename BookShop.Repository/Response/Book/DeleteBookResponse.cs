
using BookShop.Repository.Interfaces;
using BookShop.Repository.Request.Book;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Repository.Response.Book
{
   
    /// <summary>
    /// 
    /// </summary>
    public class DeleteBookResponse: IMapFrom<CreateBookRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
       


    }
}
