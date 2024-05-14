using PrOjEkT2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrOjEkT2.Controllers
{
    [Authorize]
    public class SvecenikController : Controller
    {
        BazaDbContext bazaPodataka = new BazaDbContext();

        // GET: Svecenik
        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.Title = "Početna O Popisu Svecenika";
            ViewBag.Misa = "Dodavanje Svecenika:";

            return View();
        }
        [AllowAnonymous]
        public ActionResult Popis(string naziv)
        {
            var svecenik = bazaPodataka.PopisSvecenik.ToList();

            if (!String.IsNullOrWhiteSpace(naziv))
                svecenik = svecenik.Where(x => x.Ime.ToUpper().Contains(naziv.ToUpper())).ToList();

            return View(svecenik);
        }

        public ActionResult Dodaj()
        {
            return View();
        }

        public ActionResult Azuriraj(int? id)
        {

            List<string> lista = new List<string>();
            foreach (Crkva z in bazaPodataka.PopisCrkvi)
            {
                lista.Add(z.Ime);
            }
            ViewBag.Lista = lista;
            TempData["lista"] = lista;

            Svecenik svecenik = null;
            if (!id.HasValue)
            {
                svecenik = new Svecenik();
                ViewBag.Title = "Kreiranje Svecenika";
                ViewBag.Novi = true;
            }
            else
            {
                svecenik = bazaPodataka.PopisSvecenik.FirstOrDefault(x => x.Id == id);

                if (svecenik == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Title = "Azuriranje podataka o Sveceniku";
                ViewBag.Novi = false;
                TempData["brojac"] = 1;
            }
            TempData["ZupaIme"] = svecenik.zupa;
            TempData["crkva"] = svecenik.crkva;
            return View(svecenik);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Azuriraj(Svecenik m)
        {
            //--
            List<string> lista1 = TempData["lista"] as List<string>;
            ViewBag.Lista = lista1;
            TempData["lista"] = lista1;

            string zupaIme = TempData["ZupaIme"] as string;
            if (ModelState.IsValid)
            {

                var crkva = bazaPodataka.PopisCrkvi.Any(x => x.Ime == m.crkva);
                if (!crkva)
                {
                    ModelState.AddModelError("crkva", "Neispravno Ime Crkve!");
                    ViewBag.Title = "Azuriranje podataka o Sveceniku";
                    ViewBag.Novi = true;

             

                    return View(m);
                }
                

                if (m.Id != 0)
                {
                    bazaPodataka.Entry(m).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {

                    bazaPodataka.PopisSvecenik.Add(m);
                }
                Crkva crkva1 = bazaPodataka.PopisCrkvi.FirstOrDefault(x => x.Ime == m.crkva);

                Zupa zupa = bazaPodataka.PopisZupa.FirstOrDefault(x => x.Ime == m.zupa);
                Zupa zupa1 = bazaPodataka.PopisZupa.FirstOrDefault(x => x.Ime == zupaIme);

                m.zupa = bazaPodataka.PopisZupa.FirstOrDefault(x => x.Ime == crkva1.zupa).Ime;

                if (zupa != null && zupa != zupa1)
                {
                    zupa.BrojSvecenika += 1;
                    if (zupa1 != null)
                        zupa1.BrojSvecenika -= 1;
                }
                //--


               
                Svecenik svecenik1 = new Svecenik();   
                foreach (Svecenik c in bazaPodataka.PopisSvecenik)
                {
                    if (c.Ime == m.Ime && c.Prezime == m.Prezime)
                        svecenik1 = c;
                } 
                if (m.crkva== svecenik1.crkva)
                {
                    ModelState.AddModelError("ime", "Svecenik je vec zapisan za tu crkvu!");
                    ViewBag.Title = "Svecenik je vec zapisan za tu crkvu!";
                    ViewBag.Novi = true;


                    return View(m);
                }


                bazaPodataka.SaveChanges();

                return RedirectToAction("Popis");
            }
            if (m.Id == 0)
            {
                ViewBag.Title = "Kreiranje Svecenika";
                ViewBag.Novi = true;
            }
            else
            {
                ViewBag.Title = "Azuriranje podataka o Sveceniku";
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
            Svecenik m = bazaPodataka.PopisSvecenik.FirstOrDefault(x => x.Id == id);

            if (m == null)
            {
                return HttpNotFound();
            }
            ViewBag.Title = "Potvrda brisanja Svecenika";
            return View(m);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Brisi(int id)
        {
            Svecenik m = bazaPodataka.PopisSvecenik.FirstOrDefault(x => x.Id == id);
            if (m == null)
            {
                return HttpNotFound();
            }

            bazaPodataka.PopisSvecenik.Remove(m);
            bazaPodataka.SaveChanges();

            return View("BrisiStatus");
        }
    }
}

