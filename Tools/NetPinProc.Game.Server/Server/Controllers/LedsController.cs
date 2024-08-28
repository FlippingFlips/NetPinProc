using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetPinProc.Domain.MachineConfig;
using NetPinProc.Game.Sqlite;

namespace NetPinProc.Game.Manager.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LedsController : NetProcBaseController
    {
        public LedsController(ILogger<LedsController> logger, 
            INetProcDbContext netProcDb) : base(logger, netProcDb) { }

        /// <summary>Gets all the leds in database</summary>
        /// <returns></returns>
        public async Task<IEnumerable<LedConfigFileEntry>> OnGetAsync() => 
            await _netProcDb.Leds.ToListAsync();

        /// <summary>Updates a led in database</summary>
        /// <param name="ledConfig"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> OnPutAsync([FromBody] LedConfigFileEntry ledConfig)
        {
            try
            {
                _netProcDb.Leds.Update(ledConfig);
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
