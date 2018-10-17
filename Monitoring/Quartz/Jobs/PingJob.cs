using Quartz;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Monitoring.Data;
using Monitoring.Domain.Entities;

namespace Monitoring.Quartz.Jobs
{
    /// <summary>
    /// Работа проверяющая доступность сайта
    /// </summary>
    public class PingJob : BaseJob
    {
        protected override async Task OnExecuteAsync(IJobExecutionContext context, IServiceProvider serviceProvider)
        {
            context.JobDetail.JobDataMap.TryGetValue("SiteInfoId", out var siteInfoId);
            if (siteInfoId == null)
            {
                return;
            }

            var appDbContext = serviceProvider.GetRequiredService<AppDbContext>();

            var siteInfo = await appDbContext.Set<SiteInfo>().FindAsync(siteInfoId);
            if (siteInfo == null)
            {
                return;
            }

            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(siteInfo.Url);
                    siteInfo.IsAvailable = response.IsSuccessStatusCode;
                }
                catch (HttpRequestException)
                {
                    siteInfo.IsAvailable = false;
                }
                finally
                {
                    siteInfo.StatusUpdateTime = DateTime.UtcNow;
                }
            }

            await appDbContext.SaveChangesAsync();
        }
    }
}
