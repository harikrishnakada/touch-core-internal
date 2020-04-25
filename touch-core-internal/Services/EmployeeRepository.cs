using AutoMapper;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<ServiceResponse<GetEmployeeDTO>> AddNewEmployeeAsync(AddEmployeeDTO newEmployee)
        {
            var serviceResponse = new ServiceResponse<GetEmployeeDTO>();

            var employeeByEmail = await this.GetEmployeeByEmailAsync(newEmployee.Email);
            if (employeeByEmail.Data != null)
            {
                serviceResponse.UpdateResponseStatus($"Employee with email {newEmployee.Email} already exists.", false);

                return serviceResponse;
            }

            var employee = this.Mapper.Map<Employee>(newEmployee);
            await this.DataContext.Employees.AddAsync(employee);
            await this.DataContext.SaveChangesAsync();

            serviceResponse.Data = this.Mapper.Map<GetEmployeeDTO>(employee);
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

        public async Task<ServiceResponse<GetEmployeeDTO>> GetEmployeeByEmailAsync(string email)
        {
            var serviceResponse = new ServiceResponse<GetEmployeeDTO>();
            var dbEmployee = await DataContext.Employees.FirstOrDefaultAsync(x => x.Email == email);

            serviceResponse.Data = this.Mapper.Map<GetEmployeeDTO>(dbEmployee);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetEmployeeDTO>> GetEmployeeByIdAsync(Guid id)
        {
            var serviceResponse = new ServiceResponse<GetEmployeeDTO>();
            var dbEmployee = await DataContext.Employees
                .Include(x => x.TimeSheets)
                .Include(x => x.Rewards)
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