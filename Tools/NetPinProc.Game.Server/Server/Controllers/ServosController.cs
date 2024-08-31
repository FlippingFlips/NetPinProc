using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetPinProc.Domain.Exceptions;
using NetPinProc.Domain.MachineConfig;
using NetPinProc.Game.Sqlite;

namespace NetPinProc.Game.Manager.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServosController : NetProcBaseController
    {
        public ServosController(ILogger<ServosController> logger, 
            INetProcDbContext netProcDb) : base(logger, netProcDb) { }

        /// <summary>Gets all the Servos in database</summary>
        /// <returns></returns>
        public async Task<IEnumerable<ServoConfigFileEntry>> OnGetAsync() => 
            await _netProcDb.Servos.ToListAsync();

        [HttpPost]
        public async Task<ActionResult<ServoConfigFileEntry>> OnPostAsync([FromBody] ServoConfigFileEntry swConfig)
        {
            try
            {
                _netProcDb.Servos.Add(swConfig);
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
        public async Task<ActionResult<ServoConfigFileEntry>> OnPutAsync([FromBody] ServoConfigFileEntry swConfig)
        {
            try
            {
                _netProcDb.Servos.Update(swConfig);
                await _netProcDb.SaveChangesAsync();

                return Ok(swConfig);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest($"{ex.Message} - {ex.InnerException?.Message}");
            }
        }

        [HttpDelete("{name}")]
        public async Task<ActionResult<int>> OnDeleteAsync(string name)
        {
            try
            {
                var item = await _netProcDb.Servos.FirstOrDefaultAsync(x => x.Name == name);

                if (item == null) throw new ServoNotFoundException($"{name} doesn't exist to delete!");

                _netProcDb.Servos.Remove(item);

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
