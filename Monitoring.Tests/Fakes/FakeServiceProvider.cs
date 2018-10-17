using System;
using System.Collections.Generic;

namespace Monitoring.Tests.Fakes
{
    public class FakeServiceProvider : IServiceProvider
    {
        private IDictionary<Type, object> Services { get; set; } = new Dictionary<Type, object>();

        public object GetService(Type serviceType) 
            => Services.TryGetValue(serviceType, out var service) 
                ? service 
                : null;
    }
}
