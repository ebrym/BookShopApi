
using BookShop.Data;
using BookShop.Repository.Interfaces;
using BookShop.Repository.Request.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Repository.Authentication.Commands
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso>
    /// <cref>MediatR.IRequestHandler{Application.Request.User.AuthenticateRequest, (System.Boolean Succeed, System.String Message, System.Object user)}</cref>
    /// </seealso>
    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateRequest, (bool Succeed, string Message, object user)>
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IApplicationDbContext applicationDbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticateCommandHandler"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="signInManager">The sign in manager.</param>
        /// <param name="applicationDbContext"></param>
        public AuthenticateCommandHandler(UserManager<User> userManager, SignInManager<User> signInManager, IApplicationDbContext applicationDbContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.applicationDbContext = applicationDbContext;
        }
        /// <summary>
        /// Handles a request
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        /// Response from the request
        /// </returns>
        public async Task<(bool Succeed, string Message, object user)> Handle(AuthenticateRequest request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null) user = await userManager.FindByNameAsync(request.Username);
            if (user != null)
            {
                if (user.IsDeleted)
                {
                    return (false, "The user has been deleted", null);
                }

                var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: true);
                if (result.Succeeded)
                {


                        return (true, "User has been authenticated successfully", user);

                 


                }
                else if (result.IsLockedOut)
                {
                    return (false, "The user has been locked out", null);
                }
                else if (result.RequiresTwoFactor)
                {
                    return (false, "The requires two factor authentication", user);
                }
                else if (result.IsNotAllowed)
                {
                    return (false, "The user is not allowed", null);
                }
            }
            else
            {
                return (false, "Invalid login  attempt ", null);
            }

            return (false, "Unable to login user, invalid request", null);
        }
    }
}
