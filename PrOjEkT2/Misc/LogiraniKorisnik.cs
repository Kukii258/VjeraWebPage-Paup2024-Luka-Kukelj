using PrOjEkT2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace PrOjEkT2.Misc
{
    public class LogiraniKorisnik : IPrincipal
    {
        public string KorisnickoIme { get; set; }
        public string PrezimeIme { get; set; }
        public string Ovlast { get; set; }

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            if (Ovlast == role) return true;
            return false;
        }

        public LogiraniKorisnik(Vjernik kor)
        {
            this.Identity = new GenericIdentity(kor.korisnicko_ime);
            this.KorisnickoIme = kor.korisnicko_ime;
            this.PrezimeIme = kor.PrezimeIme;
            this.Ovlast = kor.SifraOvlasti;
        }

        public LogiraniKorisnik(string korisnickoIme)
        {
            this.Identity = new GenericIdentity(korisnickoIme);
            this.KorisnickoIme = korisnickoIme;
        }
    }
}