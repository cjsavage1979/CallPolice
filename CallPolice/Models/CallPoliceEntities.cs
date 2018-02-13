using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
namespace CallPolice.Models
{
    public class CallPoliceEntities : DbContext
    {
        public DbSet<Users> Users { get; set; }

        public DbSet<PoliceNews> News { get; set; }

        public DbSet<Alarms> Alarms { get; set; }
    }
}