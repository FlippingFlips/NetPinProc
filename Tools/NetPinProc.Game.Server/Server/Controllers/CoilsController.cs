using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetPinProc.Domain.MachineConfig;
using NetPinProc.Game.Sqlite;

namespace NetPinProc.Game.Manager.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoilsController : NetProcBaseController
    {
        public CoilsController(ILogger<CoilsController> logger, INetProcDbContext netProcDb) : base(logger, netProcDb)
        {
        }

        /// <summary>Gets all the coils in database</summary>
        /// <returns></returns>
        public async Task<IEnumerable<CoilConfigFileEntry>> OnGetAsync() =>
            await _netProcDb.Coils.ToListAsync();

        /// <summary>Updates a coil in database</summary>
        /// <param name="coilConfig"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> OnPutAsync([FromBody] CoilConfigFileEntry coilConfig)
        {
            try
            {
                _netProcDb.Coils.Update(coilConfig);
                await _netProcDb.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }
        }
    }
}
