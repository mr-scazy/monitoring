using Monitoring.Domain.Enums;
using Quartz;

namespace Monitoring.Quartz
{
    public class TriggerSettings
    {
        public string Name { get; set; }

        public int Interval { get; set; }

        public IntervalUnit IntervalUnit { get; set; }
    }
}
