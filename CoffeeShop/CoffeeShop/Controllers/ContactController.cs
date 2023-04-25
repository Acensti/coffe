using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using CoffeeShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Controllers
{
    public class ContactController : Controller
    {
        [HttpPost]
        public ActionResult Submit(ContactForm contactForm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress("youremail@example.com");
                    mailMessage.To.Add("recipient@example.com");
                    mailMessage.Subject = "New contact form submission";
                    mailMessage.Body = $"Name: {contactForm.Name}\nEmail: {contactForm.Email}\n\nMessage:\n{contactForm.Message}";

                    SmtpClient smtpClient = new SmtpClient
                    {
                        Host = "smtp.example.com", // Replace with your SMTP server
                        Port = 587,
                        Credentials = new System.Net.NetworkCredential("username", "password"), // Replace with your credentials
                        EnableSsl = true,
                    };

                    smtpClient.Send(mailMessage);

                    return Json(new { success = true });
                }
                catch
                {
                    return Json(new { success = false });
                }
            }
            return Json(new { success = false });
        }
        
    }
}