using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using touch_core_internal.DTOs;
using touch_core_internal.Services;

namespace touch_core_internal.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController : ControllerBase
    {
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this.EmployeeRepository = employeeRepository;
        }

        public IEmployeeRepository EmployeeRepository { get; set; }

        [Route("{id:guid}"), HttpDelete]
        public virtual async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (!ModelState.IsValid)
                return this.BadRequest(ModelState);

            var serviceResponse = await this.EmployeeRepository.DeleteEmployeeAsync(id);

            if (serviceResponse.Data == null)
                return this.NotFound("Employee not found");

            return this.Ok(serviceResponse);
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAll()
        {
            var serviceResponse = await this.EmployeeRepository.GetAllEmployeesAsync();
            return this.Ok(serviceResponse);
        }

        [Route("{id:guid?}"), HttpGet]
        public virtual async Task<IActionResult> GetByIdAsync(Guid? id)
        {
            var serviceResponse = new ServiceResponse<GetEmployeeDTO>();
            if (id.HasValue)
            {
                serviceResponse = await this.EmployeeRepository.GetEmployeeByIdAsync(id.Value);
                if (serviceResponse.Data == null)
                {
                    serviceResponse.UpdateResponseStatus($"Employee does not exist", false);
                    return this.NotFound(serviceResponse);
                }
                serviceResponse.UpdateResponseStatus($"Employee exist");
                return this.Ok(serviceResponse);
            }
            else
            {
                serviceResponse.UpdateResponseStatus($"Employee does not exist", false);
                return this.NotFound("");
            }
        }

        [Route("username/{username}"), HttpGet]
        public virtual async Task<IActionResult> GetByNameAsync(string username)
        {
            ServiceResponse<GetEmployeeDTO> serviceResponse = new ServiceResponse<GetEmployeeDTO>();
            if (!string.IsNullOrEmpty(username))
            {
                serviceResponse = await EmployeeRepository.GetEmployeeByNameAsync(username);
                if (serviceResponse.Data == null)
                {
                    serviceResponse.UpdateResponseStatus($"Employee with {username} does not exist", false);
                    return this.NotFound(serviceResponse);
                }
                serviceResponse.UpdateResponseStatus($"Employee { username } exist");
                return this.Ok(serviceResponse);
            }
            else
            {
                serviceResponse.UpdateResponseStatus($"Employee does not exist", false);
                return this.NotFound(serviceResponse);
            }
        } 
        
        [Route("email/{email}"), HttpGet]
        public virtual async Task<IActionResult> GetByEmailAsync(string email)
        {
            ServiceResponse<GetEmployeeDTO> serviceResponse = new ServiceResponse<GetEmployeeDTO>();
            if (!string.IsNullOrEmpty(email))
            {
                serviceResponse = await EmployeeRepository.GetEmployeeByEmailAsync(email);
                if (serviceResponse.Data == null)
                {
                    serviceResponse.UpdateResponseStatus($"Employee with {email} does not exist", false);
                    return this.NotFound(serviceResponse);
                }
                serviceResponse.UpdateResponseStatus($"Employee { email } exist");
                return this.Ok(serviceResponse);
            }
            else
            {
                serviceResponse.UpdateResponseStatus($"Employee does not exist", false);
                return this.NotFound(serviceResponse);
            }
        }

        [Route("update"), HttpPost]
        public virtual async Task<IActionResult> UpdateEmployee(UpdateEmployeeDTO updateEmployee)
        {
            if (!ModelState.IsValid)
                return this.BadRequest(ModelState);

            var serviceResponse = await this.EmployeeRepository.UpdateEmployeeAsync(updateEmployee);
            return this.Ok(serviceResponse);
        }

        [Route("upsert"), HttpPost]
        public virtual async Task<IActionResult> UpsertEmployee(AddEmployeeDTO newEmployee)
        {
            if (!ModelState.IsValid)
                return this.BadRequest(ModelState);

            var serviceResponse = await this.EmployeeRepository.AddNewEmployeeAsync(newEmployee);
            return this.Ok(serviceResponse);
        }
    }
}