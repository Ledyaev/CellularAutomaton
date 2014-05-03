using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CellularAutomaton.Context.Migrations;
using CellularAutomaton.Domain;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CellularAutomaton.Context
{
    public class CellularAutomatonContext: IdentityDbContext
    {
        public CellularAutomatonContext()
            : base("CellularAutomatonContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CellularAutomatonContext, Configuration>());
        }

        public DbSet<Message> Messages { get; set; }
    }
}
