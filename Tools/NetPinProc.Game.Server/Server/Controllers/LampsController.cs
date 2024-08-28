using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetPinProc.Domain.MachineConfig;
using NetPinProc.Game.Sqlite;

namespace NetPinProc.Game.Manager.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LampsController : NetProcBaseController
    {
        public LampsController(ILogger<LampsController> logger, 
            INetProcDbContext netProcDb) : base(logger, netProcDb) { }

        /// <summary>Gets all the leds in database</summary>
        /// <returns></returns>
        public async Task<IEnumerable<LampConfigFileEntry>> OnGetAsync() => 
            await _netProcDb.Lamps.ToListAsync();

        /// <summary>Updates a led in database</summary>
        /// <param name="lampConfig"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> OnPutAsync([FromBody] LampConfigFileEntry lampConfig)
        {
            try
            {
                _netProcDb.Lamps.Update(lampConfig);
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
