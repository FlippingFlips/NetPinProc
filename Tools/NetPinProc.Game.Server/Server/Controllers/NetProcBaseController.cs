using Microsoft.AspNetCore.Mvc;
using NetPinProc.Game.Sqlite;

namespace NetPinProc.Game.Manager.Server.Controllers
{
    public class NetProcBaseController : ControllerBase
    {
        protected readonly ILogger<NetProcBaseController> _logger;
        protected readonly INetProcDbContext _netProcDb;

        public NetProcBaseController(ILogger<NetProcBaseController> logger, INetProcDbContext netProcDb)
        {
            _logger = logger;
            _netProcDb = netProcDb;
        }
    }
}
