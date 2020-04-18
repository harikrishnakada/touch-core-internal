using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using touch_core_internal.DTOs;

namespace touch_core_internal.Services
{
    public interface IEmployeeRepository
    {
        Task<ServiceResponse<List<GetEmployeeDTO>>> AddNewEmployeeAsync(AddEmployeeDTO newEmployee);

        Task<ServiceResponse<List<GetEmployeeDTO>>> DeleteEmployeeAsync(Guid id);

        Task<ServiceResponse<List<GetEmployeeDTO>>> GetAllEmployeesAsync();

        Task<ServiceResponse<GetEmployeeDTO>> GetEmployeeByIdAsync(Guid id);

        Task<ServiceResponse<GetEmployeeDTO>> GetEmployeeByNameAsync(string name);

        Task<ServiceResponse<GetEmployeeDTO>> UpdateEmployeeAsync(UpdateEmployeeDTO updateEmployee);
    }
}