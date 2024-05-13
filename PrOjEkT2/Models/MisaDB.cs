using PrOjEkT2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrOjEkT2.Models
{
    public class MisaDB
    {
        private static List<Misa> lista = new List<Misa>();
        private static bool listaInicijalizirana = false;

        public MisaDB()
        {
            if (listaInicijalizirana == false)
            {
                listaInicijalizirana = true;
                lista.Add(new Misa()
                {
                    Id = 1,
                    Ime = "1 Nedelja Dosasca",
                    DatumVrijemeMise = new DateTime(2024, 05, 05),
                    IdCrkva = 1,
                    IdZupa = 1,
                    IdSvecenik = 1
                });
                lista.Add(new Misa()
                {
                    Id = 1,
                    Ime = "1 Nedelja Dosasca",
                    DatumVrijemeMise = new DateTime(2024, 05, 05),
                    IdCrkva = 1,
                    IdZupa = 1,
                    IdSvecenik = 1
                });
                lista.Add(new Misa()
                {
                    Id = 1,
                    Ime = "1 Nedelja Dosasca",
                    DatumVrijemeMise = new DateTime(2024, 05, 05),
                    IdCrkva = 1,
                    IdZupa = 1,
                    IdSvecenik = 1
                });
                lista.Add(new Misa()
                {
                    Id = 1,
                    Ime = "1 Nedelja Dosasca",
                    DatumVrijemeMise = new DateTime(2024, 05, 05),
                    IdCrkva = 1,
                    IdZupa = 1,
                    IdSvecenik = 1
                }
                );
            }
        }

        public List<Misa> VratiListu()
        {
            return lista;
        }

        public void AzurirajMisu(Misa misa)
        {
            int misaIndex = lista.FindIndex(x => x.Id == misa.Id);
            lista[misaIndex] = misa;
        }
    }
}