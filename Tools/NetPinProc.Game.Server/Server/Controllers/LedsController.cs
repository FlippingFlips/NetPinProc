using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetPinProc.Domain.Exceptions;
using NetPinProc.Domain.MachineConfig;
using NetPinProc.Game.Sqlite;

namespace NetPinProc.Game.Manager.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LedsController : NetProcBaseController
    {
        public LedsController(ILogger<LedsController> logger,
                 INetProcDbContext netProcDb) : base(logger, netProcDb) { }

        public async Task<IEnumerable<LedConfigFileEntry>> OnGetAsync() =>
            await _netProcDb.Leds.ToListAsync();

        [HttpPost]
        public async Task<ActionResult<LedConfigFileEntry>> OnPostAsync(
            [FromBody] LedConfigFileEntry ledConfig)
        {
            try
            {
                _netProcDb.Leds.Add(ledConfig);
                await _netProcDb.SaveChangesAsync();

                return Ok(ledConfig);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest($"{ex.Message} - {ex.InnerException?.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<LedConfigFileEntry>> OnPutAsync(
            [FromBody] LedConfigFileEntry ledConfig)
        {
            try
            {
                _netProcDb.Leds.Update(ledConfig);
                await _netProcDb.SaveChangesAsync();

                return Ok(ledConfig);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest($"{ex.Message} - {ex.InnerException?.Message}");
            }
        }

        [HttpDelete("{lampNum}")]
        public async Task<ActionResult<int>> OnDeleteAsync(string lampNum)
        {
            try
            {
                var item = await _netProcDb.Leds.FirstOrDefaultAsync(x => x.Number == lampNum);

                if (item == null) throw new LedNotFoundException($"{lampNum} doesn't exist to delete!");

                _netProcDb.Leds.Remove(item);

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
