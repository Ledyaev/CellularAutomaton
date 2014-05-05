using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CellularAutomaton.Context.Interfaces
{
    public interface IContext
    {
        DbSet Set(Type entityType);
        int SaveChanges();
        void Dispose();
        DbEntityEntry Entry(object entity);
    }
}
