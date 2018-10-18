#if DEBUG
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Monitoring.Data;

namespace Monitoring.WebHost.Controllers
{
    [Route("api/[controller]")]
    public class MaintenanceController : BaseController
    {
        private readonly AppDbContext _appDbContext;
        public MaintenanceController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet("ApplyMigrations")]

        public ActionResult ApplyMigrations(string targetMigration = null)
        {
            _appDbContext.Database.Migrate();

            return Content("OK");
        }
    }
}
#endif
