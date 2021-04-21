using System;
using System.Collections.Generic;

namespace BookShop.Data
{
    public class BookAuthor
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string BookId { get; set; }
        public Book Book { get; set; }
        public string AuthorId { get; set; }
        public Author Author { get; set; }




    }
}
