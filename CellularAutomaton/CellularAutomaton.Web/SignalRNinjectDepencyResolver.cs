﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Ninject;

namespace CellularAutomaton.Web
{
    public class SignalRNinjectDependencyResolver : DefaultDependencyResolver
{
    private readonly IKernel _kernel;
 
    public SignalRNinjectDependencyResolver(IKernel kernel)
    {
        _kernel = kernel;
    }
 
    public override object GetService(Type serviceType)
    {
        return _kernel.TryGet(serviceType) ?? base.GetService(serviceType);
    }
 
    public override IEnumerable<object> GetServices(Type serviceType)
    {
        return _kernel.GetAll(serviceType).Concat(base.GetServices(serviceType));
    }
}
}