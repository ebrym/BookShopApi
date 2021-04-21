using AspNet.Security.OpenIdConnect.Primitives;
using MediatR;

namespace BookShop.Repository.Request.User
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="OpenIdConnectRequest" />
    /// <seealso>
    /// <cref>
    /// MediatR.IRequest{(bool Succeed, string Message, object user)}
    /// </cref>
    /// </seealso>
    public class AuthenticateRequest : OpenIdConnectRequest, IRequest<(bool Succeed, string Message, object user)>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticateRequest"/> class.
        /// </summary>
        public AuthenticateRequest()
        {
            GrantType = "password";

        }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }
    }
}
