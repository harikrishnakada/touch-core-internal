using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using touch_core_internal.Model;
using touch_core_internal.ORM.Nhibernate;
using touch_core_internal.ViewModel;

namespace touch_core_internal.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController : ControllerBase
    {
        [Route("{id:guid}"), HttpDelete]
        public virtual async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (!ModelState.IsValid)
                return this.BadRequest(ModelState);

            using (var session = NhibernateExtensions.SessionFactory.OpenSession())
            {
                var employee = session.Get<Employee>(id);
                if (employee == null)
                    return this.NotFound();

                session.Delete(employee);
                session.Flush();
            }
            return await Task.FromResult(this.Ok()).ConfigureAwait(false);
        }

        [Route("{id:guid?}"), HttpGet]
        public virtual async Task<IActionResult> GetAsync(Guid? id)
        {
            using (var session = NhibernateExtensions.SessionFactory.OpenSession())
            {
                if (id.HasValue)
                {
                    var employee = session.QueryOver<Employee>()
                   .Where(x => x.Id == id)
                   .SingleOrDefault();

                    if (employee == null)
                        return this.NotFound();

                    return await Task.FromResult(this.Ok(employee)).ConfigureAwait(false);
                }

                var employees = session.QueryOver<Employee>()
                    .List<Employee>();

                return await Task.FromResult(this.Ok(employees)).ConfigureAwait(false);
            }
        }

        [Route("{id:guid?}"), HttpPost]
        public virtual async Task<IActionResult> UpsertEmployeeAsync(Employee viewModel, Guid? id)
        {
            if (!ModelState.IsValid)
                return this.BadRequest(ModelState);

            var isUpdate = id.HasValue;
            var employee = new Employee();
            using (var session = NhibernateExtensions.SessionFactory.OpenSession())
            {
                if (isUpdate)
                {
                    employee = session.Get<Employee>(id);
                    //TODO: We need to finalize design for view model and model
                    this.UpdateExistingEmployee(employee, viewModel);
                }
                else
                {
                    employee = viewModel;
                }
                session.SaveOrUpdate(employee);
                session.Flush();
            }
            return await Task.FromResult(this.Ok(employee.Id)).ConfigureAwait(false);
        }

        private void UpdateExistingEmployee(Employee existingEmployee, Employee viewModel)
        {
            existingEmployee.Designation = viewModel.Designation;
            existingEmployee.Email = viewModel.Email;
            existingEmployee.Identifier = viewModel.Identifier;
            existingEmployee.Name = viewModel.Name;
            existingEmployee.Photo = viewModel.Photo;
        }
    }
}