using Blog.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace Blog.Helpers
{
    public class EmailHelper
    {
        public static string ConfiguredEmail = WebConfigurationManager.AppSettings["emailfrom"];

        public static async Task ComposeEmailAsync(EmailModel email)
        {
            try
            {

                var senderEmail = $"{email.FromEmail}<{ConfiguredEmail}>";
                var mailMsg = new MailMessage(senderEmail, ConfiguredEmail)
                {
                    Subject = email.Subject,
                    Body = $"<strong>{email.FromName} has sent you the following message </strong> <hr/> {email.ComBody}",
                    IsBodyHtml = true
                };

                var svc = new PersonalEmail();
                await svc.SendAsync(mailMsg);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await Task.FromResult(0);
            }
        }
    }
};