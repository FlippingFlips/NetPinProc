using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetPinProc.Domain.Exceptions;
using NetPinProc.Domain.MachineConfig;
using NetPinProc.Game.Sqlite;
using NetPinProc.Game.Sqlite.Model;

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
        public async Task<ILookup<string, Adjustment>> OnGetAsync() => await Task.FromResult(_netProcDb.AdjustmentLookUp);
    }
}
