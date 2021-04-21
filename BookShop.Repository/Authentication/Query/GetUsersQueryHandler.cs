
using BookShop.Data;
using BookShop.Repository.Request.User;
using BookShop.Repository.Response.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Repository.Authentication.Query
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{GetUsersRequest, CreateUserResponse[]}" />
    public class GetUsersQueryHandler : IRequestHandler<GetUsersRequest, CreateUserResponse[]>
    {
        private readonly UserManager<User> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetUsersQueryHandler"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        public GetUsersQueryHandler(UserManager<User> userManager)
        {
            this.userManager = userManager;
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
        public async Task<CreateUserResponse[]> Handle(GetUsersRequest request, CancellationToken cancellationToken)
        {
            var user = await userManager.Users.Where(x => !x.IsDeleted).OrderByDescending(a => a.DateCreated).Select(x => new CreateUserResponse
            {
                Id = x.Id,
                Email = x.Email,
                FullName = x.FullName,
                Phone = x.PhoneNumber,
                UserName = x.UserName
            }).ToArrayAsync();

            return user;
        }
    }
}
