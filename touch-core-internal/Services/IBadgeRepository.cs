using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using touch_core_internal.DTOs;

namespace touch_core_internal.Services
{
    public interface IBadgeRepository
    {
        Task<ServiceResponse<List<GetBadgeDTO>>> AddNewBadgeAsync(AddBadgeDTO newBadge);

        Task<ServiceResponse<List<GetBadgeDTO>>> DeleteBadgeAsync(Guid id);

        Task<ServiceResponse<List<GetBadgeDTO>>> GetAllBadgesAsync();

        Task<ServiceResponse<GetBadgeDTO>> GetBadgeByIdAsync(Guid id);
    }
}