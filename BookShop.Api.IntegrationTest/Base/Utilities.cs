
using BookShop.Data;
using BookShop.Infrastructure;
using System;

namespace BookShop.API.IntegrationTests.Base
{
    public class Utilities
    {
        public static void InitializeDbForTests(ApplicationDbContext context)
        {
            var fiction = "B0788D2F-8003-43C1-92A4-EDC76A7C5DDE";
            var business = "6313179F-7837-473A-A4D5-A5571B43E6A6";

            context.Categories.Add(new Category
            {
                Id = fiction,
                Title = "Fiction"
            });
            context.Categories.Add(new Category
            {
                Id = business,
                Title = "Businesses"
            });
          

            context.SaveChanges();
        }
    }
}
