using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetPinProc.Domain.MachineConfig;
using NetPinProc.Game.Sqlite;

namespace NetPinProc.Game.Manager.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SwitchesController : NetProcBaseController
    {
        public SwitchesController(ILogger<SwitchesController> logger, 
            INetProcDbContext netProcDb) : base(logger, netProcDb) { }

        /// <summary>Gets all the switches in database</summary>
        /// <returns></returns>
        public async Task<IEnumerable<SwitchConfigFileEntry>> OnGetAsync() => 
            await _netProcDb.Switches.ToListAsync();

        [HttpPut]
        public async Task<bool> OnPutAsync([FromBody] SwitchConfigFileEntry swConfig)
        {
            try
            {
                _netProcDb.Switches.Update(swConfig);
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
