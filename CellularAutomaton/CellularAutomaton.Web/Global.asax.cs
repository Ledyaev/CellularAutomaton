using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CellularAutomaton.DependencyResolver;

namespace CellularAutomaton.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            ModelValidatorProviders.Providers.Clear();
            System.Web.Mvc.DependencyResolver.SetResolver(new CellularAutomaton.DependencyResolver.DependencyResolver());
            //ModelValidatorProviders.Providers.Add(new ClientDataTypeModelValidatorProvider());
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
