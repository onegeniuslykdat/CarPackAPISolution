using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarParkService.Model
{
    public class MainDb : DbContext
    {
        public MainDb(DbContextOptions _dbContextOptions) : base(_dbContextOptions)
        {
            Database.EnsureCreated();
        }

        public DbSet<PackingRules> PackingRules { get; set; }

        public DbSet<PackingTicket> PackingTickets { get; set; }
        
    }
}
