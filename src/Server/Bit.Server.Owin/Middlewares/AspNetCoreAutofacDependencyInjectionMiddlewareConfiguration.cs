﻿using Autofac;
using Autofac.Integration.Owin;
using Bit.Owin.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;

namespace Bit.Owin.Middlewares
{
    public class AspNetCoreAutofacDependencyInjectionMiddlewareConfiguration : IOwinMiddlewareConfiguration
    {
        public virtual void Configure(IAppBuilder owinApp)
        {
            owinApp.Use<AspNetCoreAutofacDependencyInjectionMiddleware>();

            owinApp.Use<AutofacScopeBasedDependencyResolverMiddleware>();
        }
    }

    public class AspNetCoreAutofacDependencyInjectionMiddleware : OwinMiddleware
    {
        public AspNetCoreAutofacDependencyInjectionMiddleware(OwinMiddleware next)
            : base(next)
        {

        }

        public override Task Invoke(IOwinContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            HttpContext aspNetCoreContext = (HttpContext)context.Environment["Microsoft.AspNetCore.Http.HttpContext"];

            aspNetCoreContext.Items["OwinContext"] = context;

            ILifetimeScope autofacScope = aspNetCoreContext.RequestServices.GetRequiredService<ILifetimeScope>();

            context.SetAutofacLifetimeScope(autofacScope);

            return Next.Invoke(context);
        }
    }
}
