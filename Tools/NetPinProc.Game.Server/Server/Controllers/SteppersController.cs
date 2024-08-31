using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetPinProc.Domain.Exceptions;
using NetPinProc.Domain.MachineConfig;
using NetPinProc.Game.Sqlite;

namespace NetPinProc.Game.Manager.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SteppersController : NetProcBaseController
    {
        public SteppersController(ILogger<SteppersController> logger, 
            INetProcDbContext netProcDb) : base(logger, netProcDb) { }

        /// <summary>Gets all the Steppers in database</summary>
        /// <returns></returns>
        public async Task<IEnumerable<StepperConfigFileEntry>> OnGetAsync() => 
            await _netProcDb.Steppers.ToListAsync();

        [HttpPost]
        public async Task<ActionResult<StepperConfigFileEntry>> OnPostAsync([FromBody] StepperConfigFileEntry swConfig)
        {
            try
            {
                _netProcDb.Steppers.Add(swConfig);
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
        public async Task<ActionResult<StepperConfigFileEntry>> OnPutAsync([FromBody] StepperConfigFileEntry swConfig)
        {
            try
            {
                _netProcDb.Steppers.Update(swConfig);
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
                var item = await _netProcDb.Steppers.FirstOrDefaultAsync(x => x.Name == name);

                if (item == null) throw new StepperNotFoundException($"{name} doesn't exist to delete!");

                _netProcDb.Steppers.Remove(item);

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
