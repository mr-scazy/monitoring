using System.Threading.Tasks;
using Monitoring.Domain.Dto;
using Monitoring.Domain.Entities;

namespace Monitoring.Services
{
    /// <summary>
    /// Интерфейс сервиса информации по сайтам
    /// </summary>
    public interface ISiteInfoService
    {
        /// <summary>
        /// Полуить резуьтат списка информации по сайтам
        /// </summary>
        Task<ListDataResult<SiteInfoDto>> GetSiteInfoDtoListAsync();

        /// <summary>
        /// Полуить резуьтат списка информации по сайтам и планированию работы
        /// </summary>
        Task<ListDataResult<SiteInfoScheduleDto>> GetSiteInfoScheduleDtoListAsync();

        /// <summary>
        /// Создать запись информации по сайту
        /// </summary>
        /// <param name="dto">Информация по сайту и планированию работы</param>
        Task<SiteInfoScheduleDto> CreateAsync(SiteInfoScheduleDto dto);

        /// <summary>
        /// Обновить запись информации по сайту
        /// </summary>
        /// <param name="dto">Информация по сайту и планированию работы</param>
        Task<SiteInfoScheduleDto> UpdateAsync(SiteInfoScheduleDto dto);

        /// <summary>
        /// Удалить запись информации по сайту
        /// </summary>
        /// <param name="id">Идентификатор <see cref="SiteInfo"/></param>
        Task DeleteAsync(long id);
    }
}
