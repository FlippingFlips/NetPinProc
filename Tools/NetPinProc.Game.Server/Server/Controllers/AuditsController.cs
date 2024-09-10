using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetPinProc.Domain.Data;
using NetPinProc.Domain.Exceptions;
using NetPinProc.Game.Sqlite;

namespace NetPinProc.Game.Manager.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuditsController : NetProcBaseController
    {
        public AuditsController(ILogger<AuditsController> logger,
            INetProcDbContext netProcDb) : base(logger, netProcDb) { }

        /// <summary>returns the lookup created when database initialized</summary>
        /// <returns></returns>
        public async Task<IDictionary<string, Audit>> OnGetAsync() => 
            await Task.FromResult(_netProcDb.Audits.ToDictionary(x=>x.Id));

        [HttpPost]
        public async Task<ActionResult<Audit>> OnPostAsync(
            [FromBody] Audit audit)
        {
            try
            {
                _netProcDb.Audits.Add(audit);
                await _netProcDb.SaveChangesAsync();

                return Ok(audit);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest($"{ex.Message} - {ex.InnerException?.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Adjustment>> OnPutAsync(
            [FromBody] Audit audit)
        {
            try
            {
                _netProcDb.Audits.Update(audit);
                await _netProcDb.SaveChangesAsync();

                return Ok(audit);
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
                var item = await _netProcDb.Audits
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (item == null) throw 
                        new AuditNotFoundException($"{id} doesn't exist to delete!");

                _netProcDb.Audits.Remove(item);

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
