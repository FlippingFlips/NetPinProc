using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetPinProc.Domain.Exceptions;
using NetPinProc.Domain.MachineConfig;
using NetPinProc.Game.Sqlite;

namespace NetPinProc.Game.Manager.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriversController : NetProcBaseController
    {
        public DriversController(ILogger<DriversController> logger,
            INetProcDbContext netProcDb) : base(logger, netProcDb) { }

        /// <summary>Gets all the switches in database</summary>
        /// <returns></returns>
        public async Task<IEnumerable<CoilConfigFileEntry>> OnGetAsync() =>
            await _netProcDb.Coils.ToListAsync();

        [HttpPost]
        public async Task<ActionResult<CoilConfigFileEntry>> OnPostAsync(
            [FromBody] CoilConfigFileEntry coilConfig)
        {
            try
            {
                _netProcDb.Coils.Add(coilConfig);
                await _netProcDb.SaveChangesAsync();

                return Ok(coilConfig);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest($"{ex.Message} - {ex.InnerException?.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<CoilConfigFileEntry>> OnPutAsync(
            [FromBody] CoilConfigFileEntry coilConfig)
        {
            try
            {
                _netProcDb.Coils.Update(coilConfig);
                await _netProcDb.SaveChangesAsync();

                return Ok(coilConfig);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest($"{ex.Message} - {ex.InnerException?.Message}");
            }
        }

        [HttpDelete("{coilNum}")]
        public async Task<ActionResult<int>> OnDeleteAsync(string coilNum)
        {
            try
            {
                var item = await _netProcDb.Coils.FirstOrDefaultAsync(x => x.Number == coilNum);

                if (item == null) throw new DriverNotFoundException($"{coilNum} doesn't exist to delete!");

                _netProcDb.Coils.Remove(item);

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
