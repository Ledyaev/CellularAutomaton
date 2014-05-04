using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using CellularAutomaton.Context;
using CellularAutomaton.Context.Migrations;
using CellularAutomaton.Domain;

namespace CellularAutomaton.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<CellularAutomatonContext, Configuration>());
            var unitOfWork = new UnitOfWork.UnitOfWork();
            var user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                NickName = "jfkhkfgki",
                BirthDay = DateTime.Now,
                UserName = "Ssdfasdfsd"
            };
            unitOfWork.UsersRepository.Insert(user);
            unitOfWork.Save();
            
        }
    }
}
