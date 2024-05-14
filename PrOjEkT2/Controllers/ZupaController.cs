using Microsoft.Ajax.Utilities;
using PrOjEkT2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrOjEkT2Controllers
{
    [Authorize]
    public class ZupaController : Controller
    {

        BazaDbContext bazaPodataka = new BazaDbContext();

        // GET: zupa
        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.Title = "Početna O Zupa";
            ViewBag.zupa = "Dodavanje Zupa:";

            return View();
        }
        [AllowAnonymous]
        public ActionResult Popis(string naziv)
        {
            var zupa = bazaPodataka.PopisZupa.ToList();

            if (!String.IsNullOrWhiteSpace(naziv))
                zupa = zupa.Where(x => x.Ime.ToUpper().Contains(naziv.ToUpper())).ToList();

            return View(zupa);
        }

        public ActionResult Dodaj()
        {
            return View();
        }

        public ActionResult Azuriraj(int? id)
        {
            Zupa zupa = null;
            if (!id.HasValue)
            {
                zupa = new Zupa();
                ViewBag.Title = "Kreiranje Mise";
                ViewBag.Novi = true;
            }
            else
            {
                zupa = bazaPodataka.PopisZupa.FirstOrDefault(x => x.Id == id);

                if (zupa == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Title = "Azuriranje podataka o Misi";
                ViewBag.Novi = false;
                TempData["brojac"] = "1";
            }
            return View(zupa);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Azuriraj(Zupa m)
        {
            if (ModelState.IsValid)
            {

                if (m.Id != 0)
                {
                    bazaPodataka.Entry(m).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    bazaPodataka.PopisZupa.Add(m);
                }

                int brojac;
                string str = TempData["brojac"] as string;
                int.TryParse(str, out brojac);
                int i = 0;
                foreach (Zupa c in bazaPodataka.PopisZupa)
                {
                    if (c.Ime == m.Ime)
                        i++;
                }

                if (i > brojac)
                {
                    ModelState.AddModelError("ime", "Zupa s tim imenom vec postoji!");
                    ViewBag.Title = "Crkva s tim imenom vec postoji";
                    ViewBag.Novi = true;
                    return View(m);
                }   

                bazaPodataka.SaveChanges();

                return RedirectToAction("Popis");
            }
            if (m.Id == 0)
            {
                ViewBag.Title = "Kreiranje Zupe";
                ViewBag.Novi = true;
            }
            else
            {
                ViewBag.Title = "Azuriranje podataka o Zupi";
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
            Zupa m = bazaPodataka.PopisZupa.FirstOrDefault(x => x.Id == id);

            if (m == null)
            {
                return HttpNotFound();
            }
            ViewBag.Title = "Potvrda brisanja Zupa";
            return View(m);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Brisi(int id)
        {
            Zupa m = bazaPodataka.PopisZupa.FirstOrDefault(x => x.Id == id);
            if (m == null)
            {
                return HttpNotFound();
            }

            bazaPodataka.PopisZupa.Remove(m);
            bazaPodataka.SaveChanges();

            return View("BrisiStatus");
        }
    }
}