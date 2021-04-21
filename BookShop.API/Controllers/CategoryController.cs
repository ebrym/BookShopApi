
using BookShop.Api;
using BookShop.Repository.Request.Category;
using BookShop.Repository.Response.Category;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Api.Endpoints
{
    /// </summary>
    [ApiVersion("1.0")]
    public class CategoryController : BaseApiController
    {
        /// <summary>
        /// Adds a new Category
        /// </summary>
        /// <param name="CategoryRequest"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [ProducesDefaultResponseType(typeof(CreateCategoryResponse))]
        public async Task<IActionResult> AddCategoryAsync([FromBody] CreateCategoryRequest CategoryRequest)
        {
            
            (bool succeed, string message, CreateCategoryResponse CategoryResponse) = await Mediator.Send(CategoryRequest);
            if (succeed)
                return Ok(CategoryResponse.ToResponse());
            return BadRequest(message.ToResponse(false, message));
        }

        /// <summary>
        /// Deletes a new Category
        /// </summary>
        /// <param name="CategoryRequest"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        [ProducesDefaultResponseType(typeof(DeleteCategoryResponse))]
        public async Task<IActionResult> DeleteCategoryAsync(string id)
        {
            DeleteCategoryRequest deleteCategoryRequest = new DeleteCategoryRequest();
            deleteCategoryRequest.Id = id;
            (bool succeed, string message, DeleteCategoryResponse deleteCategoryResponse) = await Mediator.Send(deleteCategoryRequest);
            if (succeed)
                return Ok(deleteCategoryResponse.ToResponse());
            return BadRequest(message.ToResponse(false, message));
        }
        /// <summary>
        /// Gets all Categorys
        /// </summary>
        /// <param name="CategoryRequest"></param>
        /// <returns></returns>
        [HttpGet("get")]
        [ProducesDefaultResponseType(typeof(GetAllCategoryResponse))]
        public async Task<IActionResult> GetAllCategoryAsync()
        {
            List<GetAllCategoryResponse> Category = await Mediator.Send(new GetAllCategoryRequest());

            return Ok(Category.ToResponse());
        }

        /// <summary>
        /// Gets a specific Category
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        [ProducesDefaultResponseType(typeof(GetCategoryByIdResponse))]
        public async Task<IActionResult> GetByIdCategoryAsync(string Id)
        {
            GetCategoryByIdResponse Category = await Mediator.Send(new GetCategoryByIdRequest() { Id = Id });
            return Ok(Category.ToResponse());
        }

        /// <summary>
        /// Updates the Category
        /// </summary>
        /// <param name="CategoryRequest"></param>
        /// <returns></returns>
        [HttpPost("update")]
        [ProducesDefaultResponseType(typeof(UpdateCategoryResponse))]
        public async Task<IActionResult> UpdateCategoryAsync([FromBody] UpdateCategoryRequest CategoryRequest)
        {
            UpdateCategoryRequest update = new UpdateCategoryRequest();
            update = CategoryRequest;
            
            (bool succeed, string message, UpdateCategoryResponse CategoryResponse)  = await Mediator.Send(update);
            if (succeed)
                return Ok(CategoryResponse.ToResponse());
            return BadRequest(message.ToResponse(false, message));

           
        }


    }
}

