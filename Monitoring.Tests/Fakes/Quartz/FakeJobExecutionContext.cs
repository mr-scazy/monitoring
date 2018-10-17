using System;
using System.Collections.Generic;
using System.Threading;
using Quartz;

namespace Monitoring.Tests.Fakes.Quartz
{
    public class FakeJobExecutionContext : IJobExecutionContext
    {
        IDictionary<object, object> Map { get; set; } = new Dictionary<object, object>();

        public void Put(object key, object objectValue)
            => Map.Add(key, objectValue);

        public object Get(object key)
            => Map.TryGetValue(key, out var objectValue)
                ? objectValue
                : null;

        public IScheduler Scheduler { get; set; }
        public ITrigger Trigger { get; set; }
        public ICalendar Calendar { get; set; }
        public bool Recovering { get; set; }
        public TriggerKey RecoveringTriggerKey { get; set; }
        public int RefireCount { get; set; }
        public JobDataMap MergedJobDataMap { get; set; }
        public IJobDetail JobDetail { get; set; }
        public IJob JobInstance { get; set; }
        public DateTimeOffset FireTimeUtc { get; set; }
        public DateTimeOffset? ScheduledFireTimeUtc { get; set; }
        public DateTimeOffset? PreviousFireTimeUtc { get; set; }
        public DateTimeOffset? NextFireTimeUtc { get; set; }
        public string FireInstanceId { get; set; }
        public object Result { get; set; }
        public TimeSpan JobRunTime { get; set; }
        public CancellationToken CancellationToken { get; set; }
    }
}
