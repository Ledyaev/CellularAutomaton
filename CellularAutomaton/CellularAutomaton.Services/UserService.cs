using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CellularAutomaton.Services.Interfaces;
using CellularAutomaton.UnitOfWork.Interfaces;

namespace CellularAutomaton.Services
{
    public class UserService: IUserService
    {
        private IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
    }
}
