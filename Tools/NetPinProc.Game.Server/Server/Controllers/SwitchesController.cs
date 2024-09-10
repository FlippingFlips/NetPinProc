using Microsoft.AspNetCore.Mvc;
using NetPinProc.Domain.MachineConfig;
using NetPinProc.Game.Manager.Server.Controllers.Base;

namespace NetPinProc.Game.Manager.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SwitchesController : ConfigEntryControllerBase<SwitchConfigFileEntry>
    {
        public SwitchesController(
            ILogger<ConfigEntryControllerBase<SwitchConfigFileEntry>> logger) 
            : base(logger)
        {
        }
    }
}
