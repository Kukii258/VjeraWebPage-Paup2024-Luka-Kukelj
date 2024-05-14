using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using PrOjEkT2.Models;

namespace PrOjEkT2.Controllers
{
    [Authorize]
    public class MailController : Controller
    {
        // GET: Mail
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(mail model)
        {

            List<Vjernik> listaVjernika = TempData["vjernici"] as List<Vjernik>;

            foreach(var v in listaVjernika)
            {
                MailMessage mm = new MailMessage("paup.lukakukelj@gmail.com", v.email);
                mm.Subject = model.Subject;
                mm.Body = model.Body;
                mm.IsBodyHtml = false;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;

                NetworkCredential nc = new NetworkCredential("paup.lukakukelj@gmail.com", "shhy ggwj ajzr fmwp");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = nc;
                smtp.Send(mm);
            }

            
            ViewBag.Message = "Mail Has Been Sent Successfully";
            return RedirectToAction("Index","Home");
        }
    }
}