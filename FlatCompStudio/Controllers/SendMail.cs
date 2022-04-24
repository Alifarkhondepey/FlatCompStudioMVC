using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace FlatCompStudio.Controllers
{
    public class SendMail
    {
        public static async Task<bool> send(  string body,string email,string fullname)
        {

            var bodyemail = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p><p>PhoneNumber:{3}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress("farkhondepeyali@gmail.com"));  // recive Email : replace with valid value ContactUsReceive@morabix.com
            message.From = new MailAddress("info@flatcompstudio.com");  // Sender Email : replace with valid value
            message.Subject = "Contact FlatCompStudio";
            message.Body = string.Format(body, fullname, email, body);
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
                return true;
            }
            catch (Exception)
            {

                throw;
            }


        }

    }
}