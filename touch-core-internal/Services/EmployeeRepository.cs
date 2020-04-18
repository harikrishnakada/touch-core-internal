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
    public class EmployeeRepository : IEmployeeRepository
    {
        public EmployeeRepository(IMapper mapper, DataContext dataContext)
        {
            this.Mapper = mapper;
            this.DataContext = dataContext;
        }

        public DataContext DataContext { get; private set; }

        public IMapper Mapper { get; private set; }

        public async Task<ServiceResponse<List<GetEmployeeDTO>>> AddNewEmployeeAsync(AddEmployeeDTO newEmployee)
        {
            var serviceResponse = new ServiceResponse<List<GetEmployeeDTO>>();
            var employee = this.Mapper.Map<Employee>(newEmployee);
            await this.DataContext.Employees.AddAsync(employee);
            await this.DataContext.SaveChangesAsync();

            serviceResponse.Data = await this.DataContext.Employees
                .Include(x => x.TimeSheets)
                .Select(e => this.Mapper.Map<GetEmployeeDTO>(e))
                .ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetEmployeeDTO>>> DeleteEmployeeAsync(Guid id)
        {
            var serviceResponse = new ServiceResponse<List<GetEmployeeDTO>>();
            try
            {
                var employee = await this.DataContext.Employees.FirstAsync(x => x.EmployeeId == id);
                this.DataContext.Employees.Remove(employee);
                await this.DataContext.SaveChangesAsync();

                serviceResponse.Data = await this.DataContext.Employees
                    .Select(x => this.Mapper.Map<GetEmployeeDTO>(x))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.UpdateResponseStatus("Employee does not exist", false);
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetEmployeeDTO>>> GetAllEmployeesAsync()
        {
            var serviceResponse = new ServiceResponse<List<GetEmployeeDTO>>();
            var dbEmployees = await DataContext.Employees
                .Include(x => x.TimeSheets)
                .ToListAsync();
            serviceResponse.Data = dbEmployees.Select(c => this.Mapper.Map<GetEmployeeDTO>(c)).ToList();
            serviceResponse.UpdateResponseStatus($"Count of Employees: {serviceResponse.Data.Count}");
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetEmployeeDTO>> GetEmployeeByIdAsync(Guid id)
        {
            var serviceResponse = new ServiceResponse<GetEmployeeDTO>();
            var dbEmployee = await DataContext.Employees
                .Include(x => x.TimeSheets)
                .FirstOrDefaultAsync(x => x.EmployeeId == id);

            serviceResponse.Data = this.Mapper.Map<GetEmployeeDTO>(dbEmployee);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetEmployeeDTO>> GetEmployeeByNameAsync(string name)
        {
            var serviceResponse = new ServiceResponse<GetEmployeeDTO>();
            var dbEmployee = await DataContext.Employees.FirstOrDefaultAsync(x => x.Name == name);

            serviceResponse.Data = this.Mapper.Map<GetEmployeeDTO>(dbEmployee);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetEmployeeDTO>> UpdateEmployeeAsync(UpdateEmployeeDTO updateEmployee)
        {
            var serviceResponse = new ServiceResponse<GetEmployeeDTO>();
            try
            {
                var employee = await this.DataContext.Employees.FirstOrDefaultAsync(x => x.EmployeeId == updateEmployee.EmployeeId);
                employee.Designation = updateEmployee.Designation;
                employee.Email = updateEmployee.Email;
                employee.Identifier = updateEmployee.Identifier;
                employee.Name = updateEmployee.Name;

                this.DataContext.Employees.Update(employee);
                await this.DataContext.SaveChangesAsync();

                serviceResponse.Data = this.Mapper.Map<GetEmployeeDTO>(employee);
            }
            catch (Exception ex)
            {
                serviceResponse.UpdateResponseStatus($"Employee {updateEmployee.Name} not found", false);
            }
            return serviceResponse;
        }
    }
}