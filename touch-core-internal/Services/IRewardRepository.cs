using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using touch_core_internal.DTOs;

namespace touch_core_internal.Services
{
    public interface IRewardRepository
    {
        Task<ServiceResponse<Guid>> AddNewRewardAsync(AddRewardDTO newReward);

        Task<ServiceResponse<List<GetRewardDTO>>> GetAllRewardsAsync();

        Task<ServiceResponse<GetRewardDTO>> GetRewardByIdAsync(Guid id);
    }
}