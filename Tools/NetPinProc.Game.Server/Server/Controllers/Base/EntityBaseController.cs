using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetPinProc.Game.Manager.Shared.Common;
using NetPinProc.Game.Sqlite;

namespace NetPinProc.Game.Manager.Server.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]")]
    public class EntityBaseController<T> : ControllerBase where T : class
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<T>>> OnGetAsync(
            [FromServices] NetProcDbContext netProcDb)
        {

            IQueryable<T> query = netProcDb.Set<T>().AsQueryable();

            var paged = PaginatedList<T>.Create(query, 1, 5);

            return await netProcDb.Set<T>().ToListAsync();
        }
    }
}
