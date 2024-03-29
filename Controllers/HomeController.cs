﻿using Blog.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var publishedBlogPosts = db.BlogPosts.Where(b => b.Published).ToList();
            return View(publishedBlogPosts);
        }


        public ActionResult Contact()
        {
            EmailModel model = new EmailModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(EmailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var body = model.ComBody;
                    var from = model.FromEmail;
                    var subject = model.Subject;
                    var email = new MailMessage(from, ConfigurationManager.AppSettings["emailto"])

                {
                    Subject = "My Blog Email",
                    Body = string.Format(body, model.FromName, model.FromEmail,model.ComBody),
                    IsBodyHtml = true
                };
                    var svc = new PersonalEmail();
                    await svc.SendAsync(email);
                    return View(new EmailModel());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await Task.FromResult(0);
                }
            }
            return View(model);
        }


















    }
}