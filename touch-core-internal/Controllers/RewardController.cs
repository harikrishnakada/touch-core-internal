using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using touch_core_internal.Model;
using touch_core_internal.ORM.Nhibernate;

namespace touch_core_internal.Controllers
{
    [ApiController]
    [Route("api/reward")]
    public class RewardController : ControllerBase
    {
        [Route("{id:guid?}"), HttpGet]
        public virtual async Task<IActionResult> GetAsync(Guid? id)
        {
            using (var session = NhibernateExtensions.SessionFactory.OpenSession())
            {
                if (id.HasValue)
                {
                    var reward = session.QueryOver<Reward>()
                        .Fetch(x => x.Badge).Eager
                        .Fetch(x => x.Employee).Eager
                   .Where(x => x.Id == id)
                   .SingleOrDefault();

                    if (reward == null)
                        return this.NotFound();

                    return await Task.FromResult(this.Ok(reward)).ConfigureAwait(false);
                }

                var rewards = session.QueryOver<Reward>()
                     .Fetch(x => x.Badge).Eager
                        .Fetch(x => x.Employee).Eager
                    .List<Reward>();

                return await Task.FromResult(this.Ok(rewards)).ConfigureAwait(false);
            }
        }

        [Route("{id:guid?}"), HttpPost]
        public virtual async Task<IActionResult> UpsertRewardAsync(Reward viewModel, Guid? id)
        {
            if (!ModelState.IsValid)
                return this.BadRequest(ModelState);

            var isUpdate = id.HasValue;
            var reward = new Reward();
            using (var session = NhibernateExtensions.SessionFactory.OpenSession())
            {
                if (isUpdate)
                {
                    reward = session.Get<Reward>(id);
                }
                reward.Employee = new Employee() { Id = viewModel.EmployeeId };
                reward.Badge = new Badge() { Id = viewModel.BadgeId };
                session.SaveOrUpdate(reward);
                session.Flush();
            }
            return await Task.FromResult(this.Ok(reward.Id)).ConfigureAwait(false);
        }
    }
}