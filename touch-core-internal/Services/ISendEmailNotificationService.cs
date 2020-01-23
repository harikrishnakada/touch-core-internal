using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using static touch_core_internal.Services.SendEmailNotificationService;

namespace touch_core_internal.Services
{
    public interface ISendEmailNotificationService
    {
        SmtpClient CreateClient();

        Task SendEmail(EmailNotificationDetails emailNotificationDetails);

    }
}
