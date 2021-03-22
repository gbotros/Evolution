using Evolution.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolution.Data
{
    public class EvolutionContext : DbContext
    {

        public DbSet<Animal> Animals { get; set; }
        public DbSet<Plant> Plants { get; set; }


    }


}
