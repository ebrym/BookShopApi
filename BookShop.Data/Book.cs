using System;
using System.Collections.Generic;

namespace BookShop.Data
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public DateTimeOffset Published { get; set; }
        public Category Category { get; set; }
        public IList<BookAuthor> BookAuthors { get; set; }




    }
}
