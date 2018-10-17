using System;
using System.Collections.Specialized;
using Quartz.Impl;

namespace Monitoring.Quartz
{
    class CustomSchedulerFactory : StdSchedulerFactory
    {
        public IServiceProvider ServiceProvider { get; }

        public CustomSchedulerFactory(IServiceProvider serviceProvider, NameValueCollection props) : base(props)
        {
            ServiceProvider = serviceProvider;
        }
    }
}
