using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CellularAutomaton.Context;

namespace CellularAutomaton.Services
{
    public class Initializer
    {
        public void Initialize()
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<CellularAutomatonContext>);
        }
    }
}
