using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using touch_core_internal.DTOs;

namespace touch_core_internal
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Models.Employee, GetEmployeeDTO>();
            CreateMap<Models.Employee, GetTimeSheetToEmployeeDTO>();
            CreateMap<AddEmployeeDTO, Models.Employee>();
            CreateMap<Models.TimeSheet, GetEmployeeToTimesheetDTO>();

            CreateMap<Models.TimeSheet, GetTimeSheetDTO>();
            CreateMap<AddTimeSheetDTO, Models.TimeSheet>();

            CreateMap<Models.Category, GetCategoryDTO>();
            CreateMap<AddCategoryDTO, Models.Category>();

            CreateMap<Models.Badge, GetBadgeDTO>();
            CreateMap<AddBadgeDTO, Models.Badge>();

            CreateMap<Models.Reward, GetRewardDTO>();
            CreateMap<AddRewardDTO, Models.Reward>();
        }
    }
}