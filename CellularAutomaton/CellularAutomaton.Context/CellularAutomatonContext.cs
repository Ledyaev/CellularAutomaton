using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CellularAutomaton.Domain.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CellularAutomaton.Context
{
    public class CellularAutomatonContext: IdentityDbContext
    {
        public CellularAutomatonContext()
            : base("DatingPortalContext")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<CellularAutomatonContext, Configuration>());
        }

        public DbSet<IMessage> Messages { get; set; }
    }
}
