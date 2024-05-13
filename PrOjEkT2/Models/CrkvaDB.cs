using PrOjEkT2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrOjEkT2.Models
{
    public class CrkvaDB
    {


        private static List<Crkva> lista = new List<Crkva>();
        private static bool listaInicijalizirana = false;

        public CrkvaDB()
        {
            if (listaInicijalizirana == false)
            {
                listaInicijalizirana = true;
                lista.Add(new Crkva()
                {
                    Id = 1,
                    Ime = "Crkva Sveti Petar Orehovec",
                    Adresa = "Sveti Petar Orehovec",
                    BrojVjernika = 0,
                    IdSvecenika = 1,
                    IdZupa = 1
                });
                lista.Add(new Crkva()
                {
                    Id = 2,
                    Ime = "Crkva Sveta Ana",
                    Adresa = "Krizevci 21",
                    BrojVjernika = 31,
                    IdSvecenika = 2,
                    IdZupa = 1
                });
                lista.Add(new Crkva()
                {
                    Id = 1,
                    Ime = "Crkva Sveti Nikola",
                    Adresa = "Šibenik 52",
                    BrojVjernika = 44,
                    IdSvecenika = 3,
                    IdZupa = 2
                });



            }
        }

        public List<Crkva> VratiListu()
        {
            return lista;
        }

        public void AzurirajCrkvu(Crkva crkva)
        {
            int crkvaIndex = lista.FindIndex(x => x.Id == crkva.Id);
            lista[crkvaIndex] = crkva;
        }
    }
}
