using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetPinProc.Domain.Exceptions;
using NetPinProc.Domain.MachineConfig;
using NetPinProc.Game.Sqlite;

namespace NetPinProc.Game.Manager.Server.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ConfigEntryControllerBase<T> : ControllerBase where T : ConfigFileEntryBase
    {
        private readonly ILogger<ConfigEntryControllerBase<T>> logger;

        public ConfigEntryControllerBase(ILogger<ConfigEntryControllerBase<T>> logger)
        {
            this.logger = logger;
        }

        public async Task<ActionResult<IEnumerable<T>>> OnGetAsync(
            [FromServices] NetProcDbContext netProcDbContext,
            int id)
        {
            try
            {
                return Ok(await netProcDbContext.Set<T>().ToListAsync());
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest($"No {typeof(T)} found");
            }
        }

        [HttpGet("ById/{id}")]
        public async Task<ActionResult<T>> OnGetByIdAsync(
            [FromServices] NetProcDbContext netProcDbContext,
            int id)
        {
            var result = await netProcDbContext.FindAsync<T>(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<T>> OnPostAsync(
            [FromServices] NetProcDbContext netProcDbContext,
            [FromBody] T entity)
        {
            try
            {
                netProcDbContext.Add(entity);
                await netProcDbContext.SaveChangesAsync();

                return Ok(entity);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest($"{ex.Message} - {ex.InnerException?.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<T>> OnPutAsync(
            [FromServices] NetProcDbContext netProcDbContext,
            [FromBody] T entity)
        {
            try
            {
                netProcDbContext.Update(entity);
                await netProcDbContext.SaveChangesAsync();
                return Ok(entity);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest($"{ex.Message} - {ex.InnerException?.Message}");
            }
        }


        [HttpDelete("{name}")]
        public async Task<ActionResult<int>> OnDeleteAsync(
            [FromServices] NetProcDbContext netProcDbContext,
            string name)
        {
            try
            {
                var item = await netProcDbContext.Set<T>().
                        FirstOrDefaultAsync(x => x.Name == name);

                if (item == null) throw new MachineItemNotFoundException<T>($"{typeof(T)}");

                netProcDbContext.Remove(item);

                return Ok(await netProcDbContext.SaveChangesAsync());
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest($"{ex.Message} - {ex.InnerException?.Message}");
            }
        }
    }
}
