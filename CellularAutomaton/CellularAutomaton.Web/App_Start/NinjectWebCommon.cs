using CellularAutomaton.Context;
using CellularAutomaton.Context.Interfaces;
using CellularAutomaton.Domain;
using CellularAutomaton.Repositories;
using CellularAutomaton.Repositories.Interfaces;
using CellularAutomaton.Services;
using CellularAutomaton.Services.Interfaces;
using CellularAutomaton.UnitOfWork.Interfaces;
using CellularAutomaton.Web.Hubs;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.SignalR;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(CellularAutomaton.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(CellularAutomaton.Web.App_Start.NinjectWebCommon), "Stop")]

namespace CellularAutomaton.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                GlobalHost.DependencyResolver=new SignalRNinjectDependencyResolver(kernel);
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IUserStore<User>>().To<UserStore<User>>().WithConstructorArgument("context", new CellularAutomatonContext());
            kernel.Bind<IContext>().To<CellularAutomatonContext>().InSingletonScope();
            kernel.Bind<IRepository<User>>().To<GenericRepository<User>>().InSingletonScope();
            kernel.Bind<IRepository<Message>>().To<GenericRepository<Message>>().InSingletonScope();
            kernel.Bind<IRepository<Automaton>>().To<GenericRepository<Automaton>>().InSingletonScope();
            kernel.Bind<IRepository<Tag>>().To<GenericRepository<Tag>>().InSingletonScope();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork.UnitOfWork>();
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IMessageService>().To<MessageService>();
            kernel.Bind<IAutomatonService>().To<AutomatonService>();
            kernel.Bind<ITagService>().To<TagService>();
        }        
    }
}
