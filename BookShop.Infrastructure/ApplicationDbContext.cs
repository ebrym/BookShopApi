using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data;
using BookShop.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<User, Role, string>, IApplicationDbContext
    {
        private readonly IHttpContextAccessor contextAccessor;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="contextAccessor"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor contextAccessor)
            : base(options)
        {
            this.contextAccessor = contextAccessor;
        }
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






        /// <summary>
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveChangesAsync()
        {
            //Set current user and datetime just before saving to db
            int saved = 0;
            var currentUser = "";
            if (contextAccessor != null && contextAccessor.HttpContext != null && contextAccessor.HttpContext.User != null)
            {
                currentUser = contextAccessor.HttpContext.User.Identity.Name;
            }
            var currentDate = DateTime.Now;
            try
            {
                foreach (var entry in ChangeTracker.Entries<BaseEntity>()
               .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
                {
                    if (entry.State == EntityState.Added)
                    {

                        entry.Entity.DateCreated = currentDate;
                        entry.Entity.CreatedBy = currentUser;
                        entry.Entity.DateModified = currentDate;
                        entry.Entity.ModifiedBy = currentUser;


                    }
                    if (entry.State == EntityState.Modified)
                    {
                        entry.Entity.DateModified = currentDate;
                        entry.Entity.ModifiedBy = currentUser;
                        if (entry.Entity.IsDeleted == true)
                        {
                            var IsDeletedOriginalValue = entry.OriginalValues.GetValue<bool>("IsDeleted");
                            //If the avlues changed then item was just marked for deletion
                            if (IsDeletedOriginalValue != entry.Entity.IsDeleted)
                            {
                                entry.Entity.DateDeleted = currentDate;
                                entry.Entity.DeletedBy = currentUser;
                            }

                        }
                    }
                }
                saved = await base.SaveChangesAsync();
            }
            catch (Exception Ex)
            {

            }

            return saved;
        }

        /// <summary>
        /// <para>
        /// Saves all changes made in this context to the database.
        /// </para>
        /// <para>
        /// This method will automatically call <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges" /> to discover any
        /// changes to entity instances before saving to the underlying database. This can be disabled via
        /// <see cref="P:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled" />.
        /// </para>
        /// <para>
        /// Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        /// that any asynchronous operations have completed before calling another method on this context.
        /// </para>
        /// </summary>
        /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous save operation. The task result contains the
        /// number of state entries written to the database.
        /// </returns>
        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            //Set current user and datetime just before saving to db
            int saved = 0;
            var currentUser = "";
            if (contextAccessor != null && contextAccessor.HttpContext != null && contextAccessor.HttpContext.User != null)
            {
                currentUser = contextAccessor.HttpContext.User.Identity.Name;
            }
            var currentDate = DateTime.Now;
            try
            {
                foreach (var entry in ChangeTracker.Entries<BaseEntity>()
               .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
                {

                    if (entry.State == EntityState.Added)
                    {

                        entry.Entity.DateCreated = currentDate;
                        entry.Entity.CreatedBy = currentUser;
                        entry.Entity.DateModified = currentDate;
                        entry.Entity.ModifiedBy = currentUser;


                    }
                    if (entry.State == EntityState.Modified)
                    {
                        entry.Entity.DateModified = currentDate;
                        entry.Entity.ModifiedBy = currentUser;
                        if (entry.Entity.IsDeleted == true)
                        {
                            var IsDeletedOriginalValue = entry.OriginalValues.GetValue<bool>("IsDeleted");
                            //If the avlues changed then item was just marked for deletion
                            if (IsDeletedOriginalValue != entry.Entity.IsDeleted)
                            {
                                entry.Entity.DateDeleted = currentDate;
                                entry.Entity.DeletedBy = currentUser;
                            }

                        }
                    }
                }
                saved = await base.SaveChangesAsync(cancellationToken);
            }
            catch (Exception Ex)
            {

            }

            return saved;
        }
    }
}