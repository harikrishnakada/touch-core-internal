using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using touch_core_internal.DTOs;
using touch_core_internal.Services;

namespace touch_core_internal.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.CategoryRepository = categoryRepository;
        }

        public ICategoryRepository CategoryRepository { get; set; }

        //Do we really need this api?
        //If yes, then implement cascading. Rigtnow it is not implemented
        [Route("{id:guid}"), HttpDelete]
        public virtual async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (!ModelState.IsValid)
                return this.BadRequest(ModelState);

            var serviceResponse = await this.CategoryRepository.DeleteCategoryAsync(id);

            if (serviceResponse.Data == null)
                return this.NotFound("Category not found");

            return this.Ok(serviceResponse);
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAll()
        {
            var serviceResponse = await this.CategoryRepository.GetAllCategoriesAsync();
            return this.Ok(serviceResponse);
        }

        [Route("{id:guid?}"), HttpGet]
        public virtual async Task<IActionResult> GetByIdAsync(Guid? id)
        {
            var serviceResponse = new ServiceResponse<GetCategoryDTO>();
            if (id.HasValue)
            {
                serviceResponse = await this.CategoryRepository.GetCategoryByIdAsync(id.Value);
                if (serviceResponse.Data == null)
                {
                    serviceResponse.UpdateResponseStatus($"Category does not exist", false);
                    return this.NotFound(serviceResponse);
                }
                serviceResponse.UpdateResponseStatus("Category exist");
                return this.Ok(serviceResponse);
            }
            else
            {
                serviceResponse.UpdateResponseStatus($"Category does not exist", false);
                return this.NotFound("");
            }
        }

        [Route("upsert"), HttpPost]
        public virtual async Task<IActionResult> UpsertCategory(AddCategoryDTO newCategory)
        {
            if (!ModelState.IsValid)
                return this.BadRequest(ModelState);

            var serviceResponse = await this.CategoryRepository.AddNewCategoryAsync(newCategory);
            return this.Ok(serviceResponse);
        }
    }
}