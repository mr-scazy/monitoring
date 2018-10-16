using System.Threading.Tasks;
using Monitoring.Domain.Dto;
using Monitoring.Domain.Entities;

namespace Monitoring.Services
{
    public interface ISiteInfoService
    {
        Task<ListDataResult<SiteInfoDto>> GetDtoListResultAsync();

        Task<ListDataResult<SiteInfoScheduleDto>> GetListResultAsync();

        Task<SiteInfo> CreateAsync(SiteInfoScheduleDto dto);
    }
}
