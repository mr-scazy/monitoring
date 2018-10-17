using System.Reflection;
using Monitoring.Quartz;
using Monitoring.Tests.Fakes.Quartz;
using Xunit;

namespace Monitoring.Tests.Quartz
{
    public class QuartzExtensionsTest
    {
        [Fact]
        public void QuartzExtensions_Positive()
        {
            var assembly = Assembly.GetAssembly(typeof(FakeJob));
            var jobTypes = assembly.GetQuartzJobTypes();

            Assert.True(jobTypes.Count > 0);
        }

        [Fact]
        public void QuartzExtensions_Negative()
        {
            var assembly = Assembly.GetAssembly(typeof(FactAttribute));
            var jobTypes = assembly.GetQuartzJobTypes();

            Assert.True(jobTypes.Count == 0);
        }
    }
}
