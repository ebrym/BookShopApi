
using BookShop.Repository.Interfaces;
using BookShop.Repository.Response.Category;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Repository.Request.Category
{

 

    public class DeleteCategoryRequest : IRequest<(bool Succeed, string Message, DeleteCategoryResponse Response)>, IMapFrom<BookShop.Data.Category>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }


    }
}
