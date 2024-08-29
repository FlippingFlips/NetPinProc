using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetPinProc.Domain.Data;
using NetPinProc.Domain.Exceptions;
using NetPinProc.Game.Sqlite;

namespace NetPinProc.Game.Manager.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdjustmentsController : NetProcBaseController
    {
        public AdjustmentsController(ILogger<AdjustmentsController> logger,
            INetProcDbContext netProcDb) : base(logger, netProcDb) { }

        /// <summary>returns the lookup created when database initialized</summary>
        /// <returns></returns>
        public async Task<IDictionary<string, Adjustment>> OnGetAsync() => 
            await Task.FromResult(_netProcDb.Adjustments.ToDictionary(x => x.Id));

        [HttpPost]
        public async Task<ActionResult<Adjustment>> OnPostAsync([FromBody] Adjustment swConfig)
        {
            try
            {
                _netProcDb.Adjustments.Add(swConfig);
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
        public async Task<ActionResult<Adjustment>> OnPutAsync([FromBody] Adjustment adjustment)
        {
            try
            {
                _netProcDb.Adjustments.Update(adjustment);
                await _netProcDb.SaveChangesAsync();

                return Ok(adjustment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest($"{ex.Message} - {ex.InnerException?.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> OnDeleteAsync(string id)
        {
            try
            {
                var item = await _netProcDb.Adjustments
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (item == null) throw new AdjustmentNotFoundException($"{id} doesn't exist to delete!");

                _netProcDb.Adjustments.Remove(item);

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
