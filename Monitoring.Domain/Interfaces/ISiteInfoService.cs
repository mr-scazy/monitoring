using System.Collections.Generic;
using System.Threading.Tasks;
using Monitoring.Domain.Dto;
using Monitoring.Domain.Entities;

namespace Monitoring.Domain.Interfaces
{
    public interface ISiteInfoService
    {
        Task<ListDataResult<SiteInfoDto>> GetDtoListResultAsync();

        Task<ListDataResult<SiteInfo>> GetListResultAsync();
    }
}
