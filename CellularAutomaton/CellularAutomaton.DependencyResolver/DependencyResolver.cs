using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using CellularAutomaton.Context;
using CellularAutomaton.Context.Interfaces;
using CellularAutomaton.Domain;
using CellularAutomaton.Repositories;
using CellularAutomaton.Repositories.Interfaces;
using CellularAutomaton.Services;
using CellularAutomaton.Services.Interfaces;
using CellularAutomaton.UnitOfWork.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject;
using Ninject.Syntax;
using Ninject.Parameters;
using Ninject.Syntax;


namespace CellularAutomaton.DependencyResolver
{
    public class DependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public DependencyResolver()
        {
            kernel = new StandardKernel();
            AddBindings();
        }

        private void AddBindings()
        {
            kernel.Bind<IMessageService>().To<MessageService>();
            //kernel.Bind<IContext>().To<CellularAutomatonContext>().InRequestScope();
            kernel.Bind<IUserStore<User>>().To<UserStore<User>>().WithConstructorArgument("context",new CellularAutomatonContext());
            //kernel.Rebind<ModelValidatorProvider>().To<AttributeValidatorProvider>()
            kernel.Bind<IRepository<IEntity>>().To<GenericRepository<IEntity>>().InSingletonScope();
            //kernel.Bind<IRepository<Message>>().To<GenericRepository<Message>>().InSingletonScope();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork.UnitOfWork>();
            kernel.Bind<IUserService>().To<UserService>();
            
        }


        object IDependencyResolver.GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        IEnumerable<object> IDependencyResolver.GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }

}
