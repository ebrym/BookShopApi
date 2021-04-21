using BookShop.Repository.Response.User;
using MediatR;

namespace BookShop.Repository.Request.User
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="MediatR.IRequest{CreateUserResponse[]}" />
    public class GetUsersRequest : IRequest<CreateUserResponse[]>
    {
    }
}
