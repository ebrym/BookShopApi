using System;
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

namespace BookShop.Repository.Authentication.Commands
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso>
    ///     <cref>
    ///         MediatR.IRequestHandler{Application.Request.User.CreateUserRequest, (System.Boolean succeed, System.String
    ///         message, Application.Response.User.CreateUserResponse response)}
    ///     </cref>
    /// </seealso>
    public class CreateUserCommandHandler : IRequestHandler<CreateUserRequest, (bool succeed, string message, CreateUserResponse response)>
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly IApplicationDbContext applicationDbContext;
        private readonly IMediator mediator;
        private readonly IHttpContextAccessor contextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserCommandHandler"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="roleManager">The role manager.</param>
        /// <param name="applicationDbContext"></param>
        public CreateUserCommandHandler(UserManager<User> userManager, RoleManager<Role> roleManager,
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
        public async Task<(bool succeed, string message, CreateUserResponse response)> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            

            var currentUser = contextAccessor?.HttpContext?.User?.Identity?.Name;
            var user = new User
            {
                Email = request.Email,
                UserName = request.UserName,
                PhoneNumber = request.Phone,
                FullName = request.FullName,
                CreatedBy = currentUser,
                DateCreated = DateTime.Now

            };
            var userCreated = await userManager.CreateAsync(user, request.Password);
            if (!userCreated.Succeeded)
                return (false, string.Join(",", userCreated.Errors.Select(x => x.Description)), null);
            foreach (var role in request.Roles)
            {
                if (await roleManager.RoleExistsAsync(role))
                {
                    userCreated = await userManager.AddToRoleAsync(user, role);
                   
                }
                else
                {
                    var userRole = new Role() { Name = role };
                    var newRole = await roleManager.CreateAsync(userRole);
                    if (newRole.Succeeded)
                    {
                        userCreated = await userManager.AddToRoleAsync(user, role);


                    }
                }
                if (!userCreated.Succeeded) break;
            }

            if (!userCreated.Succeeded)
            {
                // delete user   
                return (false, string.Join(",", userCreated.Errors), null);
            }
           
            return (true, "User created successfully", new CreateUserResponse
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Phone = user.PhoneNumber,
                UserName = user.UserName,
                Roles = request.Roles,

            });


        }
    }
}