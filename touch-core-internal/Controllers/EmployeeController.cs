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
    [Route("api/[controller]")]
    public class EmployeeController: ControllerBase
    {
        [HttpGet]
        [Route("{id:guid?}")]
        public virtual IActionResult Get(Guid id)
        {
            using (var session = NhibernateExtensions.sessionFactory.OpenSession())
            {
                if(id != Guid.Empty && id != null)
                {
                    var employee = session.QueryOver<Employee>()
                   .Where(x => x.Id == id)
                   .SingleOrDefault();

                    return this.Ok(employee);
                }

                var employeess = session.QueryOver<Employee>()
                    .List<Employee>();

                return this.Ok(employeess);
            }
        }

        //[HttpGet]
        //[Route("{identifier:string?}")]
        //public virtual IActionResult Update([FromBody]IViewModel<Employee> viewModel, string identifier =  null)
        //{
        //    Employee employee;

        //    using (var session = NhibernateExtensions.sessionFactory.OpenSession())
        //    {
        //        if (identifier != null)
        //        {
        //            employee = session.QueryOver<Employee>()
        //           .Where(x => x.Identifier == identifier)
        //           .SingleOrDefault();

        //            viewModel.Write(employee);

        //            session.SaveOrUpdate(employee);
        //            session.Flush();
        //            return this.Ok(employee);
        //        }

        //        employee = new Employee();

        //        return this.Ok();
        //    }
        //}
    }
}
