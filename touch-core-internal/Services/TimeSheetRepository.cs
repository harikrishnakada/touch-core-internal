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
    public class TimeSheetRepository : ITimeSheetRepository
    {
        public TimeSheetRepository(IMapper mapper, DataContext dataContext)
        {
            this.Mapper = mapper;
            this.DataContext = dataContext;
        }

        public DataContext DataContext { get; private set; }

        public IMapper Mapper { get; private set; }

        public async Task<ServiceResponse<List<GetTimeSheetDTO>>> AddNewTimeSheetAsync(AddTimeSheetDTO newTimeSheet)
        {
            var serviceResponse = new ServiceResponse<List<GetTimeSheetDTO>>();
            var timesheet = this.Mapper.Map<TimeSheet>(newTimeSheet);
            await this.DataContext.TimeSheets.AddAsync(timesheet);
            await this.DataContext.SaveChangesAsync();

            serviceResponse.Data = await this.DataContext.TimeSheets
                .Include(x => x.Employee)
                .Select(e => this.Mapper.Map<GetTimeSheetDTO>(e))
                .ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetTimeSheetDTO>>> DeleteTimeSheetAsync(Guid id)
        {
            var serviceResponse = new ServiceResponse<List<GetTimeSheetDTO>>();
            try
            {
                var timesheet = await this.DataContext.TimeSheets
                    .FirstAsync(x => x.TimeSheetId == id);

                this.DataContext.TimeSheets.Remove(timesheet);
                await this.DataContext.SaveChangesAsync();

                serviceResponse.Data = await this.DataContext.TimeSheets
                    .Select(x => this.Mapper.Map<GetTimeSheetDTO>(x))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.UpdateResponseStatus("Timesheet does not exist", false);
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetTimeSheetDTO>>> GetAllTimesheetsAsync()
        {
            var serviceResponse = new ServiceResponse<List<GetTimeSheetDTO>>();

            var dbTimeSheets = await DataContext.TimeSheets
                .Include(x => x.Employee)
                .ToListAsync();
            serviceResponse.Data = dbTimeSheets.Select(c => this.Mapper.Map<GetTimeSheetDTO>(c)).ToList();
            serviceResponse.UpdateResponseStatus($"Count of Employees: {serviceResponse.Data.Count}");
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetTimeSheetDTO>> GetTimeSheetByIdAsync(Guid id)
        {
            var serviceResponse = new ServiceResponse<GetTimeSheetDTO>();
            var dbTimeSheet = await DataContext.TimeSheets
                .Include(x => x.Employee)
                .FirstOrDefaultAsync(x => x.TimeSheetId == id);

            serviceResponse.Data = this.Mapper.Map<GetTimeSheetDTO>(dbTimeSheet);
            return serviceResponse;
        }
    }
}