using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Monitoring.Domain.Entities;

namespace Monitoring.Services
{
    public interface IScheduleJobService
    {
        Task<IList<Exception>> InitAsync();

        Task<IList<Exception>> ConfigureAsync(params ScheduleJob[] scheduleJobs);

        Type GetJobType(string name);
    }
}
