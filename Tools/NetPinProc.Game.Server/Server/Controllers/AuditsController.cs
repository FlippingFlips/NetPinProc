using Microsoft.AspNetCore.Mvc;
using NetPinProc.Domain.Data;
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
        public async Task<IDictionary<string, Audit>> OnGetAsync() => await Task.FromResult(_netProcDb.Audits.ToDictionary(x=>x.Id));
    }
}
