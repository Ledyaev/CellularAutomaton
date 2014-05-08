using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
//using CellularAutomaton.DependencyResolver;
using CellularAutomaton.Web.App_Start;
using Ninject.Web.Common;
using WebGrease.Configuration;

namespace CellularAutomaton.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
           // ModelValidatorProviders.Providers.Clear();
           // ModelValidatorProviders.Providers.Remove(ModelValidatorProviders.Providers.OfType<DataAnnotationsModelValidatorProvider>().Single());
            //System.Web.Mvc.DependencyResolver.SetResolver(new DependencyResolver.DependencyResolver());
            //NinjectWebCommon.Start();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
