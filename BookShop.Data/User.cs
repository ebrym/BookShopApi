using System;
using Microsoft.AspNetCore.Identity;

namespace BookShop.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.IdentityUser" />
    public class User :  IdentityUser
    {
        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public string CreatedBy { get; set; }
        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        /// <value>
        /// The date created.
        /// </value>
        public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.Now;
        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// Gets or sets the deleted by.
        /// </summary>
        /// <value>
        /// The deleted by.
        /// </value>
        public string DeletedBy { get; set; }
        /// <summary>
        /// Gets or sets the date deleted.
        /// </summary>
        /// <value>
        /// The date deleted.
        /// </value>
        public DateTimeOffset DateDeleted { get; set; }

        /// Gets or sets the Modified by.
        /// </summary>
        /// <value>
        /// The Modified by.
        /// </value>
        public string ModifiedBy { get; set; }
        /// <summary>
        /// Gets or sets the date Modified.
        /// </summary>
        /// <value>
        /// The date Modified.
        /// </value>
        public DateTimeOffset DateModified { get; set; }
    }
}