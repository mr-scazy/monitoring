using Monitoring.Domain.Base;
using Monitoring.Domain.Enums;
using Quartz;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Monitoring.Domain.Entities
{
    [DisplayName("Планируемая работа")]
    public class ScheduleJob : LongIdBase
    {
        public string Name { get; set; }

        public string Job { get; set; }

        public string Params { get; set; }

        public int Interval { get; set; }

        public IntervalUnit IntervalUnit { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
