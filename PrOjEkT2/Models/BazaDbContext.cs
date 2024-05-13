using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MySql.Data.EntityFramework;
using PrOjEkT2.Models;

namespace PrOjEkT2.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class BazaDbContext : DbContext
    {
        public DbSet<Misa> PopisMisa { get; set; }
        public DbSet<Crkva> PopisCrkvi { get; set; }
        public DbSet<Vjernik> PopisVjernika { get; set; }
        public DbSet<Ovlast> PopisOvlasti { get; set; }
        public DbSet<Zupa> PopisZupa { get; set; }
        public DbSet<Svecenik> PopisSvecenik { get; set; }
    }
}