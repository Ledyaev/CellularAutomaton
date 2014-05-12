using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CellularAutomaton.Context.Interfaces;
using CellularAutomaton.Context.Migrations;
using CellularAutomaton.Domain;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CellularAutomaton.Context
{
    public class CellularAutomatonContext: IdentityDbContext, IContext
    {
        public CellularAutomatonContext()
            : base("CellularAutomatonContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CellularAutomatonContext, Configuration>());
        }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Automaton> Automatons { get; set; }

        public DbSet<Tag> Tags { get; set; }

    //    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    //    {
    ////        modelBuilder.Entity<Automaton>()
    ////.HasMany(a => a.Tags)
    ////.WithMany(t => t.Automatons).WillCascadeOnDelete(false);
    //        //configure model with fluent API
    //    }
    }
}
