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
    public class BadgeRepository : IBadgeRepository
    {
        public BadgeRepository(IMapper mapper, DataContext dataContext)
        {
            this.Mapper = mapper;
            this.DataContext = dataContext;
        }

        public DataContext DataContext { get; private set; }

        public IMapper Mapper { get; private set; }

        public async Task<ServiceResponse<List<GetBadgeDTO>>> AddNewBadgeAsync(AddBadgeDTO newBadge)
        {
            var serviceResponse = new ServiceResponse<List<GetBadgeDTO>>();
            var badge = this.Mapper.Map<Badge>(newBadge);
            await this.DataContext.Badges.AddAsync(badge);
            await this.DataContext.SaveChangesAsync();

            serviceResponse.Data = await this.DataContext.Badges
                .Include(x => x.Category)
                .Select(e => this.Mapper.Map<GetBadgeDTO>(e))
                .ToListAsync();

            serviceResponse.UpdateResponseStatus("New badge added successfully");
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetBadgeDTO>>> DeleteBadgeAsync(Guid id)
        {
            var serviceResponse = new ServiceResponse<List<GetBadgeDTO>>();
            try
            {
                var badge = await this.DataContext.Badges
                    .FirstAsync(x => x.BadgeId == id);

                this.DataContext.Badges.Remove(badge);
                await this.DataContext.SaveChangesAsync();

                serviceResponse.Data = await this.DataContext.Badges
                    .Include(x => x.Category)
                    .Select(x => this.Mapper.Map<GetBadgeDTO>(x))
                    .ToListAsync();

                serviceResponse.UpdateResponseStatus("Badge deleted successfully");
            }
            catch (Exception ex)
            {
                serviceResponse.UpdateResponseStatus("Badge does not exist", false);
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetBadgeDTO>>> GetAllBadgesAsync()
        {
            var serviceResponse = new ServiceResponse<List<GetBadgeDTO>>();

            var dbBadges = await DataContext.Badges
                .Include(x => x.Category)
                .ToListAsync();

            serviceResponse.Data = dbBadges.Select(c => this.Mapper.Map<GetBadgeDTO>(c)).ToList();
            serviceResponse.UpdateResponseStatus($"Count of Badges: {serviceResponse.Data.Count}");

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetBadgeDTO>> GetBadgeByIdAsync(Guid id)
        {
            var serviceResponse = new ServiceResponse<GetBadgeDTO>();
            var dbBadge = await DataContext.Badges
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.BadgeId == id);

            serviceResponse.Data = this.Mapper.Map<GetBadgeDTO>(dbBadge);

            serviceResponse.UpdateResponseStatus("Badge exist");
            return serviceResponse;
        }
    }
}