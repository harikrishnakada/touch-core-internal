using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using NHibernate;
using touch_core_internal.Configuration;
using touch_core_internal.Model;
using touch_core_internal.ORM.Nhibernate;
using touch_core_internal.Services;

namespace touch_core_internal.Controllers
{
    [ApiController]
    [Route("api/reward")]
    public class RewardController : ControllerBase
    {
        protected ISendEmailNotificationService SendEmailNotificationService { get; set; }
        protected IInternalConfiguration InternalConfiguration { get; set; }

        public RewardController(ISendEmailNotificationService sendEmailNotificationService, IInternalConfiguration internalConfiguration)
        {
            this.SendEmailNotificationService = sendEmailNotificationService;
            this.InternalConfiguration = internalConfiguration;
        }


        [Route("{id:guid?}"), HttpGet]
        public virtual async Task<IActionResult> GetAsync(Guid? id)
        {
            var rewards = new List<Reward>();
            if (id.HasValue)
                rewards.AddRange(this.GetRewards(id.Value));
            else
                rewards.AddRange(this.GetRewards(null));

            if (rewards.Count == 0)
            {
                return this.NotFound();
            }

            return await Task.FromResult(this.Ok(rewards)).ConfigureAwait(false);
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
                    reward = session.QueryOver<Reward>()
                        .Fetch(x => x.Badge).Eager
                        .Fetch(x => x.Employee).Eager
                   .Where(x => x.Id == id)
                   .SingleOrDefault();

                    reward.Employee.Id = viewModel.EmployeeId;
                    reward.Badge.Id = viewModel.BadgeId;
                }
                else
                {
                    reward.Employee = new Employee() { Id = viewModel.EmployeeId };
                    reward.Badge = new Badge() { Id = viewModel.BadgeId };
                }
                session.SaveOrUpdate(reward);
                session.Flush();
            }

            if (reward.Id != null)
            {
                var rewards = this.GetRewards(reward.Id);
                reward = rewards.FirstOrDefault();
            }
            var client = this.SendEmailNotificationService.CreateClient();
            var configuration = this.InternalConfiguration.GetConfigurationJson();


            var rawTemplate = this.GetEmailTemplate(@"RewardTemplate.txt");

            if (!string.IsNullOrEmpty(reward.Employee.Email))
            {
                await this.SendEmailNotificationService.SendEmail(
                    new EmailNotificationDetails()
                    {
                        SenderAddress = ConfigurationManager.AppSettings["SenderEmailAddress"],
                        RecipientAddresses = new List<string>() { reward.Employee.Email },
                        CCRecipientAddresses = new List<string>() { reward.Employee.Email },
                        SubjectLine = configuration["EmailNotification"]["Rewards"]["SubjectLine"].ToString(),
                        MessageTemplate = string.Format(rawTemplate, reward.Employee.Name, reward.Badge.BadgeName, reward.Badge.Category, reward.Badge.Score),
                        SmtpClient = client,
                        entity = reward
                    });
            }
            return await Task.FromResult(this.Ok(reward.Id)).ConfigureAwait(false);
        }

        private string GetEmailTemplate(string tempalteName)
        {
            var defaultLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var fiilePath = Path.Combine(defaultLocation, $@"Services\{tempalteName}");

            string rawTemplete = string.Empty;

            if (System.IO.File.Exists(fiilePath))
            {
                rawTemplete = System.IO.File.ReadAllText(fiilePath);
                // messagebody = string.Format(str, "Prasad", "Scholar", "Gold", "100");
            }

            return rawTemplete;

        }

        protected IList<Reward> GetRewards(Guid? id)
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


                    return new List<Reward>() { reward };
                }

                var rewards = session.QueryOver<Reward>()
                     .Fetch(x => x.Badge).Eager
                        .Fetch(x => x.Employee).Eager
                    .List<Reward>();

                return rewards;
            }
        }
    }
}