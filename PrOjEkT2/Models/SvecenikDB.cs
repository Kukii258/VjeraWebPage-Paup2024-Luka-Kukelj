using PrOjEkT2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrOjEkT2.Models
{
    public class SvecenikDB
    {
        private static List<Svecenik> lista = new List<Svecenik>();


        public List<Svecenik> VratiListu()
        {
            return lista;
        }

        public void AzurirajSvecenika(Svecenik svecenik)
        {
            int svecenikIndex = lista.FindIndex(x => x.Id == svecenik.Id);
            lista[svecenikIndex] = svecenik;
        }
    }
}



