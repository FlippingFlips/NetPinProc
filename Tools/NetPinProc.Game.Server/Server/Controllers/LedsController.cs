using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetPinProc.Domain.MachineConfig;
using NetPinProc.Game.Manager.Server.Controllers.Base;
using NetPinProc.Game.Sqlite;

namespace NetPinProc.Game.Manager.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LedsController : ConfigEntryControllerBase<LedConfigFileEntry>
    {
        public LedsController(
            ILogger<ConfigEntryControllerBase<LedConfigFileEntry>> logger) 
            : base(logger)
        {
        }

        /// <summary>override hack. if you try and change primary key on this item then fails<para/>
        /// remove it if the number is different and add a new one</summary>
        /// <param name="netProcDbContext"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public override async Task<ActionResult<LedConfigFileEntry>> OnPutAsync([FromServices] NetProcDbContext netProcDbContext, [FromBody] LedConfigFileEntry entity)
        {
            var e = await netProcDbContext.Leds.FirstOrDefaultAsync(x => x.Name == entity.Name);
            if (e.Number != entity.Number)
            {
                netProcDbContext.Remove(e);
                netProcDbContext.Add(entity);
                await netProcDbContext.SaveChangesAsync();
            }
            else
            {
                netProcDbContext.Leds.Update(entity);
                await netProcDbContext.SaveChangesAsync();
            }
            return Ok(e);
        }
    }
}
