using Monitoring.Quartz;
using Monitoring.Tests.Fakes;
using Monitoring.Tests.Fakes.Quartz;
using Xunit;

namespace Monitoring.Tests.Quartz
{
    public class DIJobListenerTest
    {
        [Fact]
        public void JobToBeExecuted_Positive()
        {
            var fakeServiceProvider = new FakeServiceProvider();
            var listener = new DIJobListener(fakeServiceProvider);

            var fakeJob = new FakeJob();
            var context = new FakeJobExecutionContext{ JobInstance = fakeJob };
            listener.JobToBeExecuted(context).Wait();
            
            Assert.Equal(fakeServiceProvider, fakeJob.ServiceProvider);
        }
    }
}
