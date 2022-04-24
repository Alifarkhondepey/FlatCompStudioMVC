using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace FlatCompStudio.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Test()
        {
            return View();
        }
        public async System.Threading.Tasks.Task<ActionResult> contactus(string name, string email, string comments)
        {
            //SendMail.send(comments, email, name);
            var bodyemail = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress("flatcompstudio@gmail.com"));  // recive Email : replace with valid value ContactUsReceive@morabix.com
            message.From = new MailAddress("info@flatcompstudio.com");  // Sender Email : replace with valid value
            message.Subject = "Contact FlatCompStudio";
            message.Body = string.Format(bodyemail, name, email, comments);
            message.IsBodyHtml = true;
            try
            {
                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "info@flatcompstudio.com",  // Sender Email : replace with valid value
                        Password = "s5Jhk1!4"  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "mail.flatcompstudio.com";
                    smtp.Port = 587;
                    await smtp.SendMailAsync(message);
                }
                         return Content("<h5 style='color:#FF4E6C'>بزودی با شما تماس خواهیم گرفت</h5>");

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}