
using BookShop.Api;
using BookShop.Repository.Request.Author;
using BookShop.Repository.Response.Author;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Api.Endpoints
{
    /// </summary>
    [ApiVersion("1.0")]
    public class AuthorController : BaseApiController
    {
        /// <summary>
        /// Adds a new Author
        /// </summary>
        /// <param name="AuthorRequest"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [ProducesDefaultResponseType(typeof(CreateAuthorResponse))]
        public async Task<IActionResult> AddAuthorAsync([FromBody] CreateAuthorRequest AuthorRequest)
        {
            
            (bool succeed, string message, CreateAuthorResponse AuthorResponse) = await Mediator.Send(AuthorRequest);
            if (succeed)
                return Ok(AuthorResponse.ToResponse());
            return BadRequest(message.ToResponse(false, message));
        }

        /// <summary>
        /// Deletes a new Author
        /// </summary>
        /// <param name="AuthorRequest"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        [ProducesDefaultResponseType(typeof(DeleteAuthorResponse))]
        public async Task<IActionResult> DeleteAuthorAsync(string id)
        {
            DeleteAuthorRequest deleteAuthorRequest = new DeleteAuthorRequest();
            deleteAuthorRequest.Id = id;
            (bool succeed, string message, DeleteAuthorResponse deleteAuthorResponse) = await Mediator.Send(deleteAuthorRequest);
            if (succeed)
                return Ok(deleteAuthorResponse.ToResponse());
            return BadRequest(message.ToResponse(false, message));
        }
        /// <summary>
        /// Gets all Authors
        /// </summary>
        /// <param name="AuthorRequest"></param>
        /// <returns></returns>
        [HttpGet("get")]
        [ProducesDefaultResponseType(typeof(GetAllAuthorResponse))]
        public async Task<IActionResult> GetAllAuthorAsync()
        {
            List<GetAllAuthorResponse> Author = await Mediator.Send(new GetAllAuthorRequest());

            return Ok(Author.ToResponse());
        }

        /// <summary>
        /// Gets a specific Author
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        [ProducesDefaultResponseType(typeof(GetAuthorByIdResponse))]
        public async Task<IActionResult> GetByIdAuthorAsync(string Id)
        {
            GetAuthorByIdResponse Author = await Mediator.Send(new GetAuthorByIdRequest() { Id = Id });
            return Ok(Author.ToResponse());
        }

        /// <summary>
        /// Updates the Author
        /// </summary>
        /// <param name="AuthorRequest"></param>
        /// <returns></returns>
        [HttpPost("update")]
        [ProducesDefaultResponseType(typeof(UpdateAuthorResponse))]
        public async Task<IActionResult> UpdateAuthorAsync([FromBody] UpdateAuthorRequest AuthorRequest)
        {
            UpdateAuthorRequest update = new UpdateAuthorRequest();
            update = AuthorRequest;
            
            (bool succeed, string message, UpdateAuthorResponse AuthorResponse)  = await Mediator.Send(update);
            if (succeed)
                return Ok(AuthorResponse.ToResponse());
            return BadRequest(message.ToResponse(false, message));

           
        }


    }
}

