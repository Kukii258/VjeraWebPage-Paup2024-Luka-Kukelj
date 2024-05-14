using PrOjEkT2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrOjEkT2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        BazaDbContext bazaPodataka = new BazaDbContext();


        [AllowAnonymous]
        public ActionResult Detalji(string id)
        {
            Vjernik vjernik = bazaPodataka.PopisVjernika.FirstOrDefault(x => x.korisnicko_ime == id);

            return View(vjernik);
        }




        [AllowAnonymous]
        public ActionResult Index(string naziv)
        {
            var misa = bazaPodataka.PopisMisa.ToList();

            if (!String.IsNullOrWhiteSpace(naziv))
                misa = misa.Where(x => x.Ime.ToUpper().Contains(naziv.ToUpper())).ToList();

            return View(misa);
        }

        public ActionResult Azuriraj(int? id)
        {

            List<string> lista = new List<string>();
            foreach (Crkva z in bazaPodataka.PopisCrkvi)
            {
                lista.Add(z.Ime);
            }
            ViewBag.Lista = lista;

            List<string> lista1 = new List<string>();
            foreach (Svecenik z in bazaPodataka.PopisSvecenik)
            {
                lista1.Add(z.Ime);
            }
            ViewBag.Lista1 = lista1;



            Misa misa = null;
            if (!id.HasValue)
            {
                misa = new Misa();
                ViewBag.Title = "Kreiranje Mise";
                ViewBag.Novi = true;
            }
            else
            {
                misa = bazaPodataka.PopisMisa.FirstOrDefault(x => x.Id == id);

                if (misa == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Title = "Azuriranje podataka o Misi";
                ViewBag.Novi = false;
            }
            return View(misa);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Azuriraj(Misa m)
        {
            List<string> lista = new List<string>();
            foreach (Crkva z in bazaPodataka.PopisCrkvi)
            {
                lista.Add(z.Ime);
            }
            ViewBag.Lista = lista;

            List<string> lista1 = new List<string>();
            foreach (Svecenik z in bazaPodataka.PopisSvecenik)
            {
                lista1.Add(z.Ime);
            }
            ViewBag.Lista1 = lista1;



            if (ModelState.IsValid)
            {

                if (m.Id != 0)
                {
                    bazaPodataka.Entry(m).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    Misa misa = new Misa();
                    foreach (Misa mi in bazaPodataka.PopisMisa)
                    {
                        if (mi.Ime == m.Ime && mi.Crkva == m.Crkva && mi.DatumVrijemeMise == m.DatumVrijemeMise)
                        {
                            ModelState.AddModelError("Ime", "Ime ove mise vec postoji!");
                            ViewBag.Title = "Misa je vec zapisana!";
                            ViewBag.Novi = true;

                            return View(m);
                        }
                    }
                    bazaPodataka.PopisMisa.Add(m);
                }

                var svecenik = bazaPodataka.PopisSvecenik.Any(x => x.Ime == m.Svecenik);
                if (!svecenik)
                {
                    ModelState.AddModelError("svecenik", "Neispravno Ime Svecenika!");
                    ViewBag.Title = "Azuriranje podataka o Misi";
                    ViewBag.Novi = false;
                    return View(m);
                }
                var crkva = bazaPodataka.PopisCrkvi.Any(x => x.Ime == m.Crkva);
                if (!crkva)
                {
                    ModelState.AddModelError("crkva", "Neispravno Ime Crkve!");
                    ViewBag.Title = "Azuriranje podataka o Misi";
                    ViewBag.Novi = false;
                    return View(m);
                }

               



        


                bazaPodataka.SaveChanges();

                return RedirectToAction("Index");
            }
            if (m.Id == 0)
            {
                ViewBag.Title = "Kreiranje Mise";
                ViewBag.Novi = true;
            }
            else
            {
                ViewBag.Title = "Azuriranje podataka o Misi";
                ViewBag.Title = false;
            }
            return View(m);
        }

        public ActionResult Brisi(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Popis");
            }
            Misa m = bazaPodataka.PopisMisa.FirstOrDefault(x => x.Id == id);

            if (m == null)
            {
                return HttpNotFound();
            }
            ViewBag.Title = "Potvrda brisanja studenta";
            return View(m);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Brisi(int id)
        {
            Misa m = bazaPodataka.PopisMisa.FirstOrDefault(x => x.Id == id);
            if (m == null)
            {
                return HttpNotFound();
            }

            bazaPodataka.PopisMisa.Remove(m);
            bazaPodataka.SaveChanges();

            return View("BrisiStatus");
        }

        public ActionResult Posalji(string email, int id)
        {
            Vjernik vjernik = bazaPodataka.PopisVjernika.FirstOrDefault(x => x.korisnicko_ime == email);
            Misa misa = bazaPodataka.PopisMisa.FirstOrDefault(x => x.Id == id);

            //crkva
            Crkva crkva = bazaPodataka.PopisCrkvi.FirstOrDefault(x => x.Ime == misa.Crkva);

            List<string> lista = new List<string>();

            List<Vjernik> vjernici = bazaPodataka.PopisVjernika.Where(v => v.crkva == crkva.Ime).ToList();

            mail mail = new mail();
      

            mail.Subject = "Podsjetink za misu " + misa.Ime;
            mail.Body = "Ime mise: " + misa.Ime + ", Vrijeme Mise: " + misa.DatumVrijemeMise + "\n Svecenik: " + misa.Svecenik + ", Crkva: " + misa.Ime;
            
            TempData["vjernici"] = vjernici;

            return View(mail);
        }



    }
}
