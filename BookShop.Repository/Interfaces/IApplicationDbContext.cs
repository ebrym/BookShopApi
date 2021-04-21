using System.Threading;
using System.Threading.Tasks;
using BookShop.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Repository.Interfaces
{
   
    public interface IApplicationDbContext
    {
        /// <summary>
        /// Gets or sets the books.
        /// </summary>
        /// <value>
        /// The books.
        /// </value>
        public DbSet<Book> Books { get; set; }
        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>
        /// The categories.
        /// </value>
        public DbSet<Category> Categories { get; set; }
        /// <summary>
        /// Gets or sets the authors.
        /// </summary>
        /// <value>
        /// The authors.
        /// </value>
        public DbSet<Author> Authors { get; set; }
        /// <summary>
        /// Gets or sets the book authors.
        /// </summary>
        /// <value>
        /// The book authors.
        /// </value>
        public DbSet<BookAuthor> BookAuthors { get; set; }



        Task<int> SaveChangesAsync(CancellationToken cancellation);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();
       
    }
}