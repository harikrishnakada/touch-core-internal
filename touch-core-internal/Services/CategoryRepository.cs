using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using touch_core_internal.DTOs;
using touch_core_internal.Models;

namespace touch_core_internal.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        public CategoryRepository(IMapper mapper, DataContext dataContext)
        {
            this.Mapper = mapper;
            this.DataContext = dataContext;
        }

        public DataContext DataContext { get; private set; }

        public IMapper Mapper { get; private set; }

        public async Task<ServiceResponse<List<GetCategoryDTO>>> AddNewCategoryAsync(AddCategoryDTO newCategory)
        {
            var serviceResponse = new ServiceResponse<List<GetCategoryDTO>>();
            var category = this.Mapper.Map<Category>(newCategory);
            await this.DataContext.Categories.AddAsync(category);
            await this.DataContext.SaveChangesAsync();

            serviceResponse.Data = await this.DataContext.Categories
                .Select(e => this.Mapper.Map<GetCategoryDTO>(e))
                .ToListAsync();

            serviceResponse.UpdateResponseStatus($"Count of Categories: {serviceResponse.Data.Count}");
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCategoryDTO>>> DeleteCategoryAsync(Guid id)
        {
            var serviceResponse = new ServiceResponse<List<GetCategoryDTO>>();
            try
            {
                var category = await this.DataContext.Categories
                    .FirstAsync(x => x.CategoryId == id);

                this.DataContext.Categories.Remove(category);
                await this.DataContext.SaveChangesAsync();

                serviceResponse.Data = await this.DataContext.Categories
                    .Select(x => this.Mapper.Map<GetCategoryDTO>(x))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.UpdateResponseStatus("Category does not exist", false);
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCategoryDTO>>> GetAllCategoriesAsync()
        {
            var serviceResponse = new ServiceResponse<List<GetCategoryDTO>>();

            var dbCategories = await DataContext.Categories
                .ToListAsync();

            serviceResponse.Data = dbCategories.Select(c => this.Mapper.Map<GetCategoryDTO>(c)).ToList();
            serviceResponse.UpdateResponseStatus($"Count of Categories: {serviceResponse.Data.Count}");

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCategoryDTO>> GetCategoryByIdAsync(Guid id)
        {
            var serviceResponse = new ServiceResponse<GetCategoryDTO>();
            var dbCategory = await DataContext.Categories
                .FirstOrDefaultAsync(x => x.CategoryId == id);

            serviceResponse.Data = this.Mapper.Map<GetCategoryDTO>(dbCategory);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCategoryDTO>> GetCategoryByNameAsync(string name)
        {
            var serviceResponse = new ServiceResponse<GetCategoryDTO>();
            var dbCategory = await DataContext.Categories
                .FirstOrDefaultAsync(x => x.Name.Equals(name));

            serviceResponse.Data = this.Mapper.Map<GetCategoryDTO>(dbCategory);
            return serviceResponse;
        }
    }
}