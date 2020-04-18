using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using touch_core_internal.DTOs;

namespace touch_core_internal.Services
{
    public interface ICategoryRepository
    {
        Task<ServiceResponse<List<GetCategoryDTO>>> AddNewCategoryAsync(AddCategoryDTO newTimeSheet);

        Task<ServiceResponse<List<GetCategoryDTO>>> DeleteCategoryAsync(Guid id);

        Task<ServiceResponse<List<GetCategoryDTO>>> GetAllCategoriesAsync();

        Task<ServiceResponse<GetCategoryDTO>> GetCategoryByIdAsync(Guid id);
    }
}