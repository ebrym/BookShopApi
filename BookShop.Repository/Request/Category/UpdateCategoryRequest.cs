
using BookShop.Repository.Interfaces;
using BookShop.Repository.Response.Category;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Repository.Request.Category
{
    public class UpdateCategoryRequest : IRequest<(bool Succeed, string Message, UpdateCategoryResponse Response)>, IMapFrom<BookShop.Data.Category>
    {
        public string Id { get; set; }
        public string Title { get; set; }

    }
}
