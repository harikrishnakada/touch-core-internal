using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using touch_core_internal.DTOs;

namespace touch_core_internal.Services
{
    public interface ITimeSheetRepository
    {
        Task<ServiceResponse<List<GetTimeSheetDTO>>> AddNewTimeSheetAsync(AddTimeSheetDTO newTimeSheet);

        Task<ServiceResponse<List<GetTimeSheetDTO>>> DeleteTimeSheetAsync(Guid id);

        Task<ServiceResponse<List<GetTimeSheetDTO>>> GetAllTimesheetsAsync();

        Task<ServiceResponse<GetTimeSheetDTO>> GetTimeSheetByIdAsync(Guid id);
    }
}