using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NHibernate;
using NHibernate.Util;
using touch_core_internal.Configuration;
using touch_core_internal.Model;
using touch_core_internal.ORM.Nhibernate;
using touch_core_internal.Services;
namespace touch_core_internal.Controllers
{
    [ApiController]
    [Route("api/timeSheet")]
    public class TimeSheetController : ControllerBase
    {

        [Route("{id:guid?}"), HttpGet]
        [Route("hours/{hours}"), HttpGet]
        public virtual async Task<IActionResult> GetAsync(Guid? id, int? hours)
        {
            var timeSheets = new List<TimeSheet>();
            if (id.HasValue)
                timeSheets.AddRange(this.GetTimeSheets(id.Value));
            else
                timeSheets.AddRange(this.GetTimeSheets(null));

            if (timeSheets.Count == 0)
            {
                return this.NotFound();
            }
            if (hours != null)
            {
                timeSheets = timeSheets.Where(x => x.Hours < hours).ToList();
            }
            return await Task.FromResult(this.Ok(timeSheets)).ConfigureAwait(false);
        }

        [Route("{id:guid?}"), HttpPost]
        public virtual async Task<IActionResult> UpsertTimeSheetAsync(TimeSheet viewModel, Guid? id)
        {
            if (!ModelState.IsValid)
                return this.BadRequest(ModelState);

            var isUpdate = id.HasValue;
            var timeSheet = new TimeSheet();
            using (var session = NhibernateExtensions.SessionFactory.OpenSession())
            {
                if (isUpdate)
                {
                    timeSheet = session.QueryOver<TimeSheet>()
                         .Fetch(SelectMode.Fetch, x => x.Employee)
                   .Where(x => x.Id == id)
                   .SingleOrDefault();

                    this.WriteToTimeSheet(timeSheet, viewModel);
                    timeSheet.Employee.Id = viewModel.EmployeeId;
                }
                else
                {
                    this.WriteToTimeSheet(timeSheet, viewModel);
                    timeSheet.Employee = new Employee() { Id = viewModel.EmployeeId };
                }
                session.SaveOrUpdate(timeSheet);
                session.Flush();
            }
            return await Task.FromResult(this.Ok(timeSheet)).ConfigureAwait(false);
        }

        protected IList<TimeSheet> GetTimeSheets(Guid? id)
        {
            using (var session = NhibernateExtensions.SessionFactory.OpenSession())
            {
                if (id.HasValue)
                {
                    var timeSheet = session.QueryOver<TimeSheet>()
                        .Fetch(SelectMode.Fetch, x => x.Employee)
                   .Where(x => x.Id == id)
                   .SingleOrDefault();

                    return new List<TimeSheet>() { timeSheet };
                }

                var timeSheets = session.QueryOver<TimeSheet>()
                         .Fetch(SelectMode.Fetch, x => x.Employee)
                    .List<TimeSheet>();

                timeSheets = timeSheets.Select(x => { x.Hours = (x.ToDateTime - x.FromDateTime).Duration().TotalHours; return x; }).ToList();

                //foreach (var item in timeSheets)
                //{
                //    item.Hours = (item.ToDateTime - item.FromDateTime).Duration().TotalHours;

                //}
                return timeSheets;
            }
        }

        private void WriteToTimeSheet(TimeSheet timeSheet, TimeSheet viewModel)
        {
            timeSheet.Employee = viewModel.Employee;
            timeSheet.FromDateTime = viewModel.FromDateTime;
            timeSheet.ToDateTime = viewModel.ToDateTime;
            timeSheet.Comments = viewModel.Comments;
        }

    }
}
