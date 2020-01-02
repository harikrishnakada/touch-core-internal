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
    [Route("api/badge")]
    public class BadgeController : ControllerBase
    {
        [Route("{id:guid}"), HttpDelete]
        public virtual async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (!ModelState.IsValid)
                return this.BadRequest(ModelState);

            using (var session = NhibernateExtensions.SessionFactory.OpenSession())
            {
                var badge = session.Get<Badge>(id);
                if (badge == null)
                    return this.NotFound();

                session.Delete(badge);
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
                    var badge = session.QueryOver<Badge>()
                       .Where(x => x.Id == id)
                       .SingleOrDefault();

                    if (badge == null)
                        return this.NotFound();

                    return await Task.FromResult(this.Ok(badge)).ConfigureAwait(false);
                }

                var badges = session.QueryOver<Badge>()
                    .List<Badge>();

                return await Task.FromResult(this.Ok(badges)).ConfigureAwait(false);
            }
        }

        [Route("{id:guid?}"), HttpPost]
        public virtual async Task<IActionResult> UpsertBadgeAsync(Badge viewModel, Guid? id)
        {
            if (!ModelState.IsValid)
                return this.BadRequest(ModelState);

            var isUpdate = id.HasValue;
            var badge = new Badge();
            using (var session = NhibernateExtensions.SessionFactory.OpenSession())
            {
                if (isUpdate)
                {
                    badge = session.Get<Badge>(id);
                    this.WriteViewModel(badge, viewModel);
                }
                else
                {
                    badge = viewModel;
                }
                session.SaveOrUpdate(badge);
                session.Flush();
            }
            return await Task.FromResult(this.Ok(badge.Id)).ConfigureAwait(false);
        }

        private void WriteViewModel(Badge badge, Badge viewModel)
        {
            badge.BadgeName = viewModel.BadgeName;
            badge.Category = viewModel.BadgeName;
            badge.Score = viewModel.Score;
        }
    }
}