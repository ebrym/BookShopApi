using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data;
using BookShop.Repository.Interfaces;
using BookShop.Repository.Request.User;
using BookShop.Repository.Response.User;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Repository.Authentication.Commands
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso>
    ///     <cref>
    ///         MediatR.IRequestHandler{Application.Request.User.EditUserRequest, (System.Boolean succeed, System.String
    ///         message, Application.Response.User.EditUserResponse response)}
    ///     </cref>
    /// </seealso>
    public class EditUserCommandHandler : IRequestHandler<EditUserRequest, (bool succeed, string message, EditUserResponse response)>
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly IApplicationDbContext applicationDbContext;
        private readonly IMediator mediator;
        private readonly IHttpContextAccessor contextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditUserCommandHandler"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="roleManager">The role manager.</param>
        /// <param name="applicationDbContext"></param>
        public EditUserCommandHandler(UserManager<User> userManager, RoleManager<Role> roleManager,
            IApplicationDbContext applicationDbContext, IMediator mediator, IHttpContextAccessor contextAccessor
            )
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.applicationDbContext = applicationDbContext;
            this.mediator = mediator;
            this.contextAccessor = contextAccessor;
        }
        /// <summary>
        /// Handles a request
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        /// Response from the request
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<(bool succeed, string message, EditUserResponse response)> Handle(EditUserRequest request, CancellationToken cancellationToken)
        {
           

            var currentUser = contextAccessor?.HttpContext?.User?.Identity?.Name;
            var existingUser = await userManager.FindByNameAsync(request.UserName);
            if (existingUser == null)
            {
                existingUser = await userManager.FindByEmailAsync(request.Email);
            }
            existingUser.Email = request.Email;
            existingUser.FullName = request.FullName;
            existingUser.UserName = request.UserName;
            existingUser.PhoneNumber = request.Phone;
            existingUser.FullName = request.FullName;

            var userUpdated = await userManager.UpdateAsync(existingUser);

            if (!userUpdated.Succeeded)
                return (false, string.Join(",", userUpdated.Errors.Select(x => x.Description)), null);

            var existingUserRoles = await userManager.GetRolesAsync(existingUser) as List<string>;
            var userRoleRemoved = await userManager.RemoveFromRolesAsync(existingUser, existingUserRoles);

            if (!userRoleRemoved.Succeeded)
                return (false, string.Join(",", userRoleRemoved.Errors.Select(x => x.Description)), null);

      
            foreach (var role in request.Roles)
            {
                if (string.IsNullOrWhiteSpace(role))
                {
                    continue;
                }
                if (await roleManager.RoleExistsAsync(role))
                {
                    var userAddedToNewRole = await userManager.AddToRoleAsync(existingUser, role);
                   
                }
                else
                {
                    var userRole = new Role() { Name = role };
                    var newRole = await roleManager.CreateAsync(userRole);
                    if (newRole.Succeeded)
                    {
                        var userAddedToNewRole = await userManager.AddToRoleAsync(existingUser, role);

                    }
                }
                if (!userUpdated.Succeeded) break;
            }

            if (!userUpdated.Succeeded)
            {
                // delete user   
                return (false, string.Join(",", userUpdated.Errors), null);
            }
           
            return (true, "User edited successfully", new EditUserResponse
            {
                Id = existingUser.Id,
                Email = existingUser.Email,
                FullName = existingUser.FullName,
                Phone = existingUser.PhoneNumber,
                UserName = existingUser.UserName,
                Roles = request.Roles,

            });


        }
    }
}