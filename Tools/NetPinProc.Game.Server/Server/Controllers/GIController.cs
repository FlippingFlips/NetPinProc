using Microsoft.AspNetCore.Mvc;
using NetPinProc.Domain.MachineConfig;
using NetPinProc.Game.Manager.Server.Controllers.Base;

namespace NetPinProc.Game.Manager.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GIController : ConfigEntryControllerBase<GIConfigFileEntry>
    {
        public GIController(
            ILogger<ConfigEntryControllerBase<GIConfigFileEntry>> logger) 
            : base(logger)
        {
        }
    }
}
