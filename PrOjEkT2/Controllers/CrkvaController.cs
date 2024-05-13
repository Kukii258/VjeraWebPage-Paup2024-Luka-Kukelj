using Microsoft.Ajax.Utilities;
using PrOjEkT2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrOjEkT2.Controllers
{
    [Authorize]
    public class CrkvaController : Controller
    {

        BazaDbContext bazaPodataka = new BazaDbContext();
        // GET: Crkva
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Popis(string naziv)
        {
            var crkva = bazaPodataka.PopisCrkvi.ToList();

            if (!String.IsNullOrWhiteSpace(naziv))
                crkva = crkva.Where(x => x.Ime.ToUpper().Contains(naziv.ToUpper())).ToList();

            return View(crkva);
        }


        public ActionResult Dodaj()
        {
            return View();
        }

        public ActionResult Azuriraj(int? id)
        {
            Crkva crkva = null;
            if (!id.HasValue)
            {
                crkva = new Crkva();
                ViewBag.Title = "Kreiranje Crkve";
                ViewBag.Novi = true;
            }
            else
            {
                crkva = bazaPodataka.PopisCrkvi.FirstOrDefault(x => x.Id == id);

                if (crkva == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Title = "Azuriranje podataka o Crkvi";
                ViewBag.Novi = false;
                TempData["brojac"] = "1";
            }
            TempData["ZupaIme"] = crkva.zupa;
            return View(crkva);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Azuriraj(Crkva m)
        {
            string zupaIme = TempData["ZupaIme"] as string;
            if (ModelState.IsValid)
            {

                if (m.Id != 0)
                {
                    bazaPodataka.Entry(m).State = System.Data.Entity.EntityState.Modified;
                }

                else
                {
                    bazaPodataka.PopisCrkvi.Add(m);
                }





                var zupa2 = bazaPodataka.PopisZupa.Any(x => x.Ime == m.zupa);
                if (!zupa2)
                {
                    ModelState.AddModelError("zupa", "Zupa s tim imenom ne postoji!");
                    ViewBag.Title = "Azuriranje podataka o Zupi";
                    ViewBag.Novi = false;
                    return View(m);
                }
                int brojac;
                string str = TempData["brojac"] as string;
                int.TryParse(str, out brojac);
                int i = 0;
                foreach (Crkva c in bazaPodataka.PopisCrkvi)
                {
                    if (c.Ime == m.Ime)
                        i++;
                }

                if (i > brojac)
                {
                    ModelState.AddModelError("ime", "Neispravno Ime Crkve!");
                    ViewBag.Title = "Crkva s tim imenom vec postoji";
                    ViewBag.Novi = false;
                    return View(m);
                }


                Zupa zupa = bazaPodataka.PopisZupa.FirstOrDefault(x => x.Ime == m.zupa);
                Zupa zupa1 = bazaPodataka.PopisZupa.FirstOrDefault(x => x.Ime == zupaIme);
                if (zupa != null && zupa != zupa1)
                {
                    zupa.BrojCrkvi += 1;
                    if(zupa1 != null)
                        zupa1.BrojCrkvi -= 1;
                }
                bazaPodataka.SaveChanges();

                return RedirectToAction("Popis");
            }
            if (m.Id == 0)
            {
                ViewBag.Title = "Kreiranje Crkvi";
                ViewBag.Novi = true;
            }
            else
            {
                ViewBag.Title = "Azuriranje podataka o Crkvi";
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
            Crkva c = bazaPodataka.PopisCrkvi.FirstOrDefault(x => x.Id == id);

            if (c == null)
            {
                return HttpNotFound();
            }
            ViewBag.Title = "Potvrda brisanja Crkvi";
            return View(c);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Brisi(int id)
        {
            Crkva crkva = bazaPodataka.PopisCrkvi.FirstOrDefault(x => x.Id == id);
            if (crkva == null)
            {
                return HttpNotFound();
            }

            bazaPodataka.PopisCrkvi.Remove(crkva);
            bazaPodataka.SaveChanges();

            return View("BrisiStatus");
        }

    }
}