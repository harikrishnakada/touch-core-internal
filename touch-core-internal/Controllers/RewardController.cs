using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using touch_core_internal.DTOs;
using touch_core_internal.Services;

namespace touch_core_internal.Controllers
{
    [ApiController]
    [Route("api/reward")]
    public class RewardController : ControllerBase
    {
        public RewardController(IRewardRepository rewardRepository)
        {
            this.RewardRepository = rewardRepository;
        }

        public IRewardRepository RewardRepository { get; set; }

        [HttpGet]
        public virtual async Task<IActionResult> GetAll()
        {
            var serviceResponse = await this.RewardRepository.GetAllRewardsAsync();
            return this.Ok(serviceResponse);
        }

        [Route("{id:guid?}"), HttpGet]
        public virtual async Task<IActionResult> GetByIdAsync(Guid? id)
        {
            var serviceResponse = new ServiceResponse<GetRewardDTO>();
            if (id.HasValue)
            {
                serviceResponse = await this.RewardRepository.GetRewardByIdAsync(id.Value);
                if (serviceResponse.Data == null)
                {
                    serviceResponse.UpdateResponseStatus($"Reward does not exist", false);
                    return this.NotFound(serviceResponse);
                }
                serviceResponse.UpdateResponseStatus($"Reward exist");
                return this.Ok(serviceResponse);
            }
            else
            {
                serviceResponse.UpdateResponseStatus($"Reward does not exist", false);
                return this.NotFound(serviceResponse);
            }
        }

        [Route("upsert"), HttpPost]
        public virtual async Task<IActionResult> UpsertReward(AddRewardDTO newReward)
        {
            if (!ModelState.IsValid)
                return this.BadRequest(ModelState);

            var serviceResponse = await this.RewardRepository.AddNewRewardAsync(newReward);
            return this.Ok(serviceResponse);
        }
    }
}