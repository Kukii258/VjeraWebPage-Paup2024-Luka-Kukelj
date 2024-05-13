using PrOjEkT2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrOjEkT2.Models
{
    public class ZupaDB
    {
        private static List<Zupa> lista = new List<Zupa>();

        public List<Zupa> VratiListu()
        {
            return lista;
        }

        public void AzurirajZupu(Zupa zupa)
        {
            int zupaIndex = lista.FindIndex(x => x.Id == zupa.Id);
            lista[zupaIndex] = zupa;
        }
    }
}
