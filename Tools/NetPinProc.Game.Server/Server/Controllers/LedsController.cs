using Microsoft.AspNetCore.Mvc;
using NetPinProc.Domain.MachineConfig;
using NetPinProc.Game.Manager.Server.Controllers.Base;

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
    }
}
