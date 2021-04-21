using System;
using System.Collections.Generic;

namespace BookShop.Data
{
    public class Author : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public IList<BookAuthor> BookAuthors { get; set; }
    }
}
