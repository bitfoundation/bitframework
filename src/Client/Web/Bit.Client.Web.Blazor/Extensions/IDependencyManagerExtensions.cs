﻿using Autofac;
using Bit.Core.Implementations;
using Bit.ViewModel;
using Prism.Events;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials.Interfaces;

namespace Bit.Core.Contracts
{
    public static class IDependencyManagerExtensions
    {
        public static IDependencyManager RegisterRequiredServices(this IDependencyManager dependencyManager)
        {
            if (dependencyManager == null)
                throw new ArgumentNullException(nameof(dependencyManager));

            dependencyManager.RegisterXamarinEssentials();

            dependencyManager.Register<IEventAggregator, EventAggregator>(lifeCycle: DependencyLifeCycle.SingleInstance, overwriteExisting: false);

            dependencyManager.Register<IDateTimeProvider, DefaultDateTimeProvider>(lifeCycle: DependencyLifeCycle.SingleInstance, overwriteExisting: false);

            dependencyManager.RegisterInstance(DefaultJsonContentFormatter.Current, overwriteExisting: false);

            dependencyManager.RegisterInstance<IExceptionHandler>(BitExceptionHandler.Current);

            ((IAutofacDependencyManager)dependencyManager).GetContainerBuidler().RegisterBuildCallback(container =>
            {
                if (BitExceptionHandler.Current is BitExceptionHandler exceptionHandler)
                    exceptionHandler.ServiceProvider = container.Resolve<IServiceProvider>();
            });

            dependencyManager.RegisterInstance<ITelemetryService>(DebugTelemetryService.Current);
            dependencyManager.RegisterInstance<ITelemetryService>(ConsoleTelemetryService.Current);

            dependencyManager.GetContainerBuilder().RegisterBuildCallback(container =>
            {
                IMessageReceiver? messageReceiver = container.ResolveOptional<IMessageReceiver>();
                IConnectivity connectivity = container.Resolve<IConnectivity>();
                IVersionTracking versionTracking = container.Resolve<IVersionTracking>();

                foreach (TelemetryServiceBase telemetryService in container.Resolve<IEnumerable<ITelemetryService>>().OfType<TelemetryServiceBase>())
                {
                    if (messageReceiver != null)
                        telemetryService.MessageReceiver = messageReceiver;
                    telemetryService.Connectivity = connectivity;
                    telemetryService.VersionTracking = versionTracking;
                }
            });

            return dependencyManager;
        }
    }
}
