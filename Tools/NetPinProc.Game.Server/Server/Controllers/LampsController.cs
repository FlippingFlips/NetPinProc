using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetPinProc.Domain.Exceptions;
using NetPinProc.Domain.MachineConfig;
using NetPinProc.Game.Sqlite;

namespace NetPinProc.Game.Manager.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LampsController : NetProcBaseController
    {
        public LampsController(ILogger<LampsController> logger,
                 INetProcDbContext netProcDb) : base(logger, netProcDb) { }

        public async Task<ActionResult<IEnumerable<LampConfigFileEntry>>> OnGetAsync()
        {
            try
            {
                return Ok(await _netProcDb.Lamps?.ToListAsync());
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                return BadRequest("No lamps found");
            }
        }            

        [HttpPost]
        public async Task<ActionResult<LampConfigFileEntry>> OnPostAsync(
            [FromBody] LampConfigFileEntry lampConfig)
        {
            try
            {
                _netProcDb.Lamps.Add(lampConfig);
                await _netProcDb.SaveChangesAsync();

                return Ok(lampConfig);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest($"{ex.Message} - {ex.InnerException?.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<LampConfigFileEntry>> OnPutAsync(
            [FromBody] LampConfigFileEntry lampConfig)
        {
            try
            {
                _netProcDb.Lamps.Update(lampConfig);
                await _netProcDb.SaveChangesAsync();

                return Ok(lampConfig);
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
                var item = await _netProcDb.Lamps.FirstOrDefaultAsync(x => x.Number == lampNum);

                if (item == null) throw new LampNotFoundException($"{lampNum} doesn't exist to delete!");

                _netProcDb.Lamps.Remove(item);

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
