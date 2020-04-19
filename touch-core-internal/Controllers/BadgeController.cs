using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using touch_core_internal.DTOs;
using touch_core_internal.Services;

namespace touch_core_internal.Controllers
{
    [ApiController]
    [Route("api/badge")]
    public class BadgeController : ControllerBase
    {
        public BadgeController(IBadgeRepository badgeRepository)
        {
            this.BadgeRepository = badgeRepository;
        }

        public IBadgeRepository BadgeRepository { get; set; }

        [Route("{id:guid}"), HttpDelete]
        public virtual async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (!ModelState.IsValid)
                return this.BadRequest(ModelState);

            var serviceResponse = await this.BadgeRepository.DeleteBadgeAsync(id);

            if (serviceResponse.Data == null)
                return this.NotFound("Badge not found");

            return this.Ok(serviceResponse);
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAll()
        {
            var serviceResponse = await this.BadgeRepository.GetAllBadgesAsync();
            return this.Ok(serviceResponse);
        }

        [Route("{id:guid?}"), HttpGet]
        public virtual async Task<IActionResult> GetByIdAsync(Guid? id)
        {
            var serviceResponse = new ServiceResponse<GetBadgeDTO>();
            if (id.HasValue)
            {
                serviceResponse = await this.BadgeRepository.GetBadgeByIdAsync(id.Value);
                if (serviceResponse.Data == null)
                {
                    serviceResponse.UpdateResponseStatus($"Badge does not exist", false);
                    return this.NotFound(serviceResponse);
                }
                serviceResponse.UpdateResponseStatus($"Badge exist");
                return this.Ok(serviceResponse);
            }
            else
            {
                serviceResponse.UpdateResponseStatus($"Badge does not exist", false);
                return this.NotFound(serviceResponse);
            }
        }

        [Route("upsert"), HttpPost]
        public virtual async Task<IActionResult> UpsertBadge(AddBadgeDTO newBadge)
        {
            if (!ModelState.IsValid)
                return this.BadRequest(ModelState);

            var serviceResponse = await this.BadgeRepository.AddNewBadgeAsync(newBadge);
            return this.Ok(serviceResponse);
        }
    }
}