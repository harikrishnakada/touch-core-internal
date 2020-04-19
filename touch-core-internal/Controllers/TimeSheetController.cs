﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using touch_core_internal.DTOs;
using touch_core_internal.Services;

namespace touch_core_internal.Controllers
{
    [ApiController]
    [Route("api/timeSheet")]
    public class TimeSheetController : ControllerBase
    {
        public TimeSheetController(ITimeSheetRepository timeSheetRepository)
        {
            this.TimeSheetRepository = timeSheetRepository;
        }

        public ITimeSheetRepository TimeSheetRepository { get; set; }

        [Route("{id:guid}"), HttpDelete]
        public virtual async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (!ModelState.IsValid)
                return this.BadRequest(ModelState);

            var serviceResponse = await this.TimeSheetRepository.DeleteTimeSheetAsync(id);

            if (serviceResponse.Data == null)
                return this.NotFound("Timesheet not found");

            return this.Ok(serviceResponse);
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAll()
        {
            var serviceResponse = await this.TimeSheetRepository.GetAllTimesheetsAsync();
            return this.Ok(serviceResponse);
        }

        [Route("{id:guid?}"), HttpGet]
        public virtual async Task<IActionResult> GetByIdAsync(Guid? id)
        {
            var serviceResponse = new ServiceResponse<GetTimeSheetDTO>();
            if (id.HasValue)
            {
                serviceResponse = await this.TimeSheetRepository.GetTimeSheetByIdAsync(id.Value);
                if (serviceResponse.Data == null)
                {
                    serviceResponse.UpdateResponseStatus($"Timesheet does not exist", false);
                    return this.NotFound(serviceResponse);
                }
                serviceResponse.UpdateResponseStatus($"Timesheet exist");
                return this.Ok(serviceResponse);
            }
            else
            {
                serviceResponse.UpdateResponseStatus($"Timesheet does not exist", false);
                return this.NotFound("");
            }
        }

        [Route("upsert"), HttpPost]
        public virtual async Task<IActionResult> UpsertTimeSheet(AddTimeSheetDTO newTimeSheet)
        {
            if (!ModelState.IsValid)
                return this.BadRequest(ModelState);

            var serviceResponse = await this.TimeSheetRepository.AddNewTimeSheetAsync(newTimeSheet);
            return this.Ok(serviceResponse);
        }
    }
}