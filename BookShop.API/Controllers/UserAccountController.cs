﻿using BookShop.Data;
using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Primitives;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OpenIddict.Core;
using OpenIddict.EntityFrameworkCore.Models;
using OpenIddict.Server;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static OpenIddict.Abstractions.OpenIddictConstants;
using BookShop.Repository.Response.User;
using BookShop.Repository.Request.User;
using BookShop.Repository.Authentication.Commands;
using BookShop.Api.Models;

namespace BookShop.Api
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Api.Endpoints.BaseApiController" />
    public class UserAccountController : BaseApiController
    {
        private readonly OpenIddictApplicationManager<OpenIddictApplication> applicationManager;
        private readonly SignInManager<User> signInManager;
        private readonly IConfiguration configuration;


        /// <summary>
        /// Initializes a new instance of the <see cref="UserAccountController"/> class.
        /// </summary>
        /// <param name="applicationManager">The application manager.</param>
        /// <param name="signInManager">The sign in manager.</param>
        /// <param name="configuration">The configuration.</param>

        public UserAccountController(OpenIddictApplicationManager<OpenIddictApplication> applicationManager,
            SignInManager<User> signInManager,

            IConfiguration configuration)
        {
            this.applicationManager = applicationManager;
            this.signInManager = signInManager;
            this.configuration = configuration;

        }

        /// <summary>
        /// Deletes the users.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [ProducesDefaultResponseType(typeof(string))]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUsers(string id)
        {
            (bool succeed, string message) = await Mediator.Send(new DeleteUserCommand { Id = id });
            if (succeed)
                return Ok(message.ToResponse());
            return NotFound(message.ToResponse());

        }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <returns></returns>
        [ProducesDefaultResponseType(typeof(CreateUserResponse[]))]
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var user = await Mediator.Send(new GetUsersRequest { });
            if (user != null)
                return Ok(user.ToResponse());
            return NotFound("No user found".ToResponse());

        }
        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="createUser">The create user.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [ProducesDefaultResponseType(typeof(CreateUserResponse))]
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] CreateUserRequest createUser)
        {
            (bool succeed, string message, CreateUserResponse userResponse) = await Mediator.Send(createUser);
            if (succeed)
                return Ok(userResponse.ToResponse());
            return BadRequest(message.ToResponse());
        }
        /// <summary>
        /// Edit the user.
        /// </summary>
        /// <param name="editUser">The edit user.</param>
        /// <returns></returns>
        [ProducesDefaultResponseType(typeof(EditUserResponse))]
        [HttpPost("editUser")]
        public async Task<IActionResult> EditUser([FromBody] EditUserRequest editUser)
        {
            (bool succeed, string message, EditUserResponse userResponse) = await Mediator.Send(editUser);
            if (succeed)
                return Ok(userResponse.ToResponse());
            return BadRequest(message.ToResponse());
        }
        /// <summary>
        /// Authenticates the user.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("~/connect/token")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AuthenticateUser(AuthenticateRequest model)
        {
            if (ModelState.IsValid)
            {
                (bool Succeed, string Message, object  user) = await Mediator.Send(model);
                if (Succeed)
                {
                    var ticket = await CreateTicketAsync(model, user);
                    return SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
                }
                return BadRequest(new OpenIdConnectResponse
                {
                    Error = Errors.AccessDenied,
                    ErrorDescription = Message


                });
            }

            return BadRequest(new OpenIdConnectResponse
            {
                Error = Errors.AccessDenied,
                ErrorDescription = "Invalid request"
            });

        }
        #region Authorization code; implicit flows        
        /// <summary>
        /// Authorizes the specified request.
        /// </summary>
        /// <param name="req">The request.</param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("~/connect/authorize")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Authorize(OpenIdConnectRequest req)
        {
            //Debug.Assert(request.IsAuthorizationRequest(),
            //    "The OpenIddict binder for ASP.NET Core MVC is not registered." +
            //    "Make sure services.AddOpenIddict().AddServer().UseMvc() is correctly called.");

            var application = await applicationManager.FindByClientIdAsync(req.ClientId);
            if (application == null)
            {
                var msg = "Details concerning the calling client application cannot be found in the database";
                return BadRequest(error: new ErrowVm
                {
                    Error = Errors.InvalidClient,
                    ErrorDescription = msg
                }.ToResponse(false, msg));
            }

            return Ok(new AuthorizeVm
            {
                ApplicationName = await applicationManager.GetDisplayNameAsync(application),
                RequestId = req.RequestId,
                Scope = req.Scope
            }.ToResponse());
        }
        /// <summary>
        /// Logouts the specified request.
        /// </summary>
        /// <param name="req">The request.</param>
        /// <returns></returns>
        [HttpGet("~/connect/logout")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult Logout(OpenIdConnectRequest req)
        {
            return base.Ok(new LogoutVm
            {
                RequestId = req.RequestId
            }.ToResponse());
        }
        #endregion
        /// <summary>
        /// Creates the ticket asynchronous.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="properties">The properties.</param>
        /// <returns></returns>
        private async Task<AuthenticationTicket> CreateTicketAsync(OpenIdConnectRequest request, object usr, AuthenticationProperties properties = null)
        {
            var user = usr as User;
            var principal = await signInManager.CreateUserPrincipalAsync(user);
            var ticket = new AuthenticationTicket(principal, properties, OpenIddictServerDefaults.AuthenticationScheme);
            if (!request.IsAuthorizationCodeGrantType() && !request.IsRefreshTokenGrantType())
            {
                var resource = new List<string>();
                configuration.Bind("ApiResources", resource);
                ticket.SetScopes(new[]
                {
                    OpenIdConnectConstants.Scopes.OpenId,
                    OpenIdConnectConstants.Scopes.Email,
                    OpenIdConnectConstants.Scopes.Phone,
                    OpenIdConnectConstants.Scopes.Profile,
                    OpenIdConnectConstants.Scopes.OfflineAccess,

                    Scopes.Roles,
                    "location",

                }.Intersect(request.GetScopes()));
                var clientList = new List<string>();
                configuration.Bind("ApiResources", clientList);
                ticket.SetResources(clientList);
            }
            var identity = principal.Identity as ClaimsIdentity;


            if (ticket.HasScope(OpenIdConnectConstants.Scopes.Email))
            {
                if (!string.IsNullOrWhiteSpace(user.Email))
                    identity.AddClaim("email", user.Email, OpenIdConnectConstants.Destinations.IdentityToken);
            }
            if (ticket.HasScope(OpenIdConnectConstants.Scopes.Phone))
            {
                if (!string.IsNullOrWhiteSpace(user.PhoneNumber))
                    identity.AddClaim("phone", user.PhoneNumber, OpenIdConnectConstants.Destinations.IdentityToken);
            }

           

            foreach (var claim in ticket.Principal.Claims)
            {
                claim.SetDestinations(GetDestinations(claim, ticket));
            }






            return ticket;
        }
        /// <summary>
        /// Gets the destinations.
        /// </summary>
        /// <param name="claim">The claim.</param>
        /// <param name="ticket">The ticket.</param>
        /// <returns></returns>
        private IEnumerable<string> GetDestinations(Claim claim, AuthenticationTicket ticket)
        {
            switch (claim.Type)
            {
                case Claims.Name:
                    yield return Destinations.AccessToken;
                    if (ticket.HasScope(Scopes.Profile))
                        yield return Destinations.IdentityToken;
                    yield break;
                case Claims.Email:
                    yield return Destinations.AccessToken;

                    if (ticket.HasScope(Scopes.Email))
                        yield return Destinations.IdentityToken;

                    yield break;

                case Claims.Role:
                    yield return Destinations.AccessToken;

                    if (ticket.HasScope(Scopes.Roles))
                        yield return Destinations.IdentityToken;

                    yield break;

                case "location":
                    yield return Destinations.AccessToken;

                    //  if (ticket.HasScope("location"))
                    yield return Destinations.IdentityToken;
                    yield break;

                case "locationId":
                    yield return Destinations.AccessToken;

                    //  if (ticket.HasScope("location"))
                    yield return Destinations.IdentityToken;
                    yield break;


                // Never include the security stamp in the access and identity tokens, as it's a secret value.
                case "AspNet.Identity.SecurityStamp": yield break;

                default:
                    yield return Destinations.AccessToken;
                    yield break;
            }
        }
    }
}
