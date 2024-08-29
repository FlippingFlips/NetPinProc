using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetPinProc.Domain.Exceptions;
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

        [HttpPost]
        public async Task<ActionResult<SwitchConfigFileEntry>> OnPostAsync([FromBody] SwitchConfigFileEntry swConfig)
        {
            try
            {
                _netProcDb.Switches.Add(swConfig);
                await _netProcDb.SaveChangesAsync();

                return Ok(swConfig);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest($"{ex.Message} - {ex.InnerException?.Message}");
            }
        }

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

        [HttpDelete("{swNum}")]
        public async Task<ActionResult<int>> OnDeleteAsync(string swNum)
        {
            try
            {
                var item = await _netProcDb.Switches.FirstOrDefaultAsync(x => x.Number == swNum);

                if (item == null) throw new SwitchNotFoundException($"{swNum} doesn't exist to delete!");

                _netProcDb.Switches.Remove(item);

                return Ok(await _netProcDb.SaveChangesAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest($"{ex.Message} - {ex.InnerException?.Message}");
            }
        }

    }
}
