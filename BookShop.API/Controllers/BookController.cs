
using BookShop.Api;
using BookShop.Repository.Request.Book;
using BookShop.Repository.Response.Book;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Api.Endpoints
{
    /// </summary>
    [ApiVersion("1.0")]
    public class BookController : BaseApiController
    {
        /// <summary>
        /// Adds a new Book
        /// </summary>
        /// <param name="BookRequest"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [ProducesDefaultResponseType(typeof(CreateBookResponse))]
        public async Task<IActionResult> AddBookAsync([FromBody] CreateBookRequest BookRequest)
        {
            
            (bool succeed, string message, CreateBookResponse BookResponse) = await Mediator.Send(BookRequest);
            if (succeed)
                return Ok(BookResponse.ToResponse());
            return BadRequest(message.ToResponse(false, message));
        }

        /// <summary>
        /// Deletes a new Book
        /// </summary>
        /// <param name="BookRequest"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        [ProducesDefaultResponseType(typeof(DeleteBookResponse))]
        public async Task<IActionResult> DeleteBookAsync(string id)
        {
            DeleteBookRequest deleteBookRequest = new DeleteBookRequest();
            deleteBookRequest.Id = id;
            (bool succeed, string message, DeleteBookResponse deleteBookResponse) = await Mediator.Send(deleteBookRequest);
            if (succeed)
                return Ok(deleteBookResponse.ToResponse());
            return BadRequest(message.ToResponse(false, message));
        }
        /// <summary>
        /// Gets all Books
        /// </summary>
        /// <param name="BookRequest"></param>
        /// <returns></returns>
        [HttpGet("get")]
        [ProducesDefaultResponseType(typeof(GetAllBookResponse))]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> GetAllBookAsync()
        {
            List<GetAllBookResponse> Book = await Mediator.Send(new GetAllBookRequest());

            return Ok(Book.ToResponse());
        }

        /// <summary>
        /// Gets a specific Book
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        [ProducesDefaultResponseType(typeof(GetBookByIdResponse))]
        public async Task<IActionResult> GetByIdBookAsync(string Id)
        {
            GetBookByIdResponse Book = await Mediator.Send(new GetBookByIdRequest() { Id = Id });
            return Ok(Book.ToResponse());
        }
        /// <summary>
        /// Gets a specific Book
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        [HttpGet("Category/{Id}")]
        [ProducesDefaultResponseType(typeof(GetBookByIdResponse))]
        public async Task<IActionResult> GetBookByCategoryIdAsync(string Id)
        {
            var Book = await Mediator.Send(new GetBookByCategoryIdRequest() { CategoryId = Id });
            return Ok(Book.ToResponse());
        }

        /// <summary>
        /// Gets a specific Book
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        [HttpGet("Author/{Id}")]
        [ProducesDefaultResponseType(typeof(GetBookByIdResponse))]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new[] { "impactlevel", "pii" })]
        public async Task<IActionResult> GetBookByAuthorIdAsync(string Id)
        {
            var Book = await Mediator.Send(new GetBookByAuthorIdRequest() { AuthorId = Id });
            return Ok(Book.ToResponse());
        }

        /// <summary>
        /// Updates the Book
        /// </summary>
        /// <param name="BookRequest"></param>
        /// <returns></returns>
        [HttpPost("update")]
        [ProducesDefaultResponseType(typeof(UpdateBookResponse))]
        public async Task<IActionResult> UpdateBookAsync([FromBody] UpdateBookRequest BookRequest)
        {
            UpdateBookRequest update = new UpdateBookRequest();
            update = BookRequest;
            
            (bool succeed, string message, UpdateBookResponse BookResponse)  = await Mediator.Send(update);
            if (succeed)
                return Ok(BookResponse.ToResponse());
            return BadRequest(message.ToResponse(false, message));

           
        }


    }
}

