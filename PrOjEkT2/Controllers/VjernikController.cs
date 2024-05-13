using PrOjEkT2.Misc;
using PrOjEkT2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace PrOjEkT2.Controllers
{
    [Authorize(Roles = OvlastiKorisnik.Administrator)]
    public class VjernikController : Controller
    {

        BazaDbContext bazaPodataka = new BazaDbContext();

        // GET: Vjernik
        public ActionResult Index()
        {
            var listaKorisnika = bazaPodataka.PopisVjernika.OrderBy(x => x.SifraOvlasti).ThenBy(x => x.prezime).ToList();

            return View(listaKorisnika);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Prijava(string returnUrl)
        {
            KorisnikPrijava model = new KorisnikPrijava();
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Prijava(KorisnikPrijava model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var korisnikBaza = bazaPodataka.PopisVjernika.FirstOrDefault(x => x.korisnicko_ime == model.KorisnickoIme);
                if (korisnikBaza != null)
                {

                    string a = Misc.PasswordHelper.IzracunajHash(model.Lozinka);

                    var passwordOK = korisnikBaza.Lozinka == Misc.PasswordHelper.IzracunajHash(model.Lozinka);

                    if (passwordOK)
                    {
                        LogiraniKorisnik prijavljeniKorisnik = new LogiraniKorisnik(korisnikBaza);
                        LogiraniKorisnikSerializeModel serializeModel = new LogiraniKorisnikSerializeModel();
                        serializeModel.CopyFromUser(prijavljeniKorisnik);
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        string korisnickiPodaci = serializer.Serialize(serializeModel);

                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                            1,
                            prijavljeniKorisnik.Identity.Name,
                            DateTime.Now,
                            DateTime.Now.AddDays(1),
                            false,
                            korisnickiPodaci);

                        string ticketEncrpted = FormsAuthentication.Encrypt(authTicket);

                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, ticketEncrpted);
                        Response.Cookies.Add(cookie);

                        if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }

                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            ModelState.AddModelError("", "Neisparavno korisnicko ime ili lozinka");
            return View(model);
        }
        [OverrideAuthorization]
        [Authorize]
        public ActionResult Odjava()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult Registracija()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Registracija(Vjernik model)
        {

            bool error = false;

            if (!String.IsNullOrWhiteSpace(model.korisnicko_ime))
            {
                var korImeZauzeto = bazaPodataka.PopisVjernika.Any(x => x.korisnicko_ime == model.korisnicko_ime);
                if (korImeZauzeto)
                {
                    ModelState.AddModelError("korisnicko_ime", "Korisničko ime je već zauzeto");
                    error = true;
                }
                var crkva = bazaPodataka.PopisCrkvi.Any(x => x.Ime == model.crkva);
                if (!crkva)
                {
                    ModelState.AddModelError("crkva", "Neispravno Ime Crkve!");
                    error = true;
                }
                else
                {
                    model.zupa = bazaPodataka.PopisCrkvi.FirstOrDefault(x => x.Ime == model.crkva).zupa;
                    Crkva crkva1 = bazaPodataka.PopisCrkvi.FirstOrDefault(x => x.Ime == model.crkva);
                    crkva1.BrojVjernika += 1;

                    Zupa zupa1 = bazaPodataka.PopisZupa.FirstOrDefault(x => x.Ime == model.zupa);

                    if (zupa1 != null)
                        zupa1.BrojVjernika += 1;
                }
            }
            if (!String.IsNullOrWhiteSpace(model.email))
            {
                var emailZauzet = bazaPodataka.PopisVjernika.Any(x => x.email == model.email);
                if (emailZauzet)
                {
                    ModelState.AddModelError("email", "Email je već zauzet");
                    error = true;

                }
            }

            if (!error)
            {
                model.Lozinka = Misc.PasswordHelper.IzracunajHash(model.LozinkaUnos);
                model.SifraOvlasti = "MO";

                bazaPodataka.PopisVjernika.Add(model);
                bazaPodataka.SaveChanges();

                return View("RegistracijaOk");
            }

            var ovlasti = bazaPodataka.PopisOvlasti.OrderBy(x => x.naziv).ToList();
            ViewBag.Ovlasti = ovlasti;

            return View(model);
        }

    }
}