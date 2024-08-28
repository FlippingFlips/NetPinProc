using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetPinProc.Game.Sqlite;
using NetPinProc.Game.Sqlite.Model;

namespace NetPinProc.Game.Manager.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MachineController : NetProcBaseController
    {
        public MachineController(
            ILogger<MachineController> logger,
            INetProcDbContext netProcDb) :
            base(logger, netProcDb)
        { }

        /// <summary>Gets the first machine row</summary>
        /// <returns></returns>
        public async Task<Machine> OnGetAsync() =>
            await _netProcDb.Machine.FirstAsync();
    }
}