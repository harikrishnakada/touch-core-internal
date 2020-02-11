using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using touch_core_internal.Model;
using touch_core_internal.ORM.Nhibernate;

namespace touch_core_internal.Controllers
{
    [ApiController]
    [Route("api/timeSheet")]
    public class TimeSheetController: ControllerBase
    {

        [Route("{id:guid?}"), HttpGet]
        public virtual async Task<IActionResult> GetAsync(Guid? id)
        {
            var rewards = new List<TimeSheet>();
            if (id.HasValue)
                rewards.AddRange(this.GetTimeSheets(id.Value));
            else
                rewards.AddRange(this.GetTimeSheets(null));

            if (rewards.Count == 0)
            {
                return this.NotFound();
            }

            return await Task.FromResult(this.Ok(rewards)).ConfigureAwait(false);
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
                        .Fetch(x => x.Employee).Eager
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
                        .Fetch(x => x.Employee).Eager
                   .Where(x => x.Id == id)
                   .SingleOrDefault();

                    return new List<TimeSheet>() { timeSheet };
                }

                var timeSheets = session.QueryOver<TimeSheet>()
                        .Fetch(x => x.Employee).Eager
                    .List<TimeSheet>();

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
