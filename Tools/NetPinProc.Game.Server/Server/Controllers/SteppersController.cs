using Microsoft.AspNetCore.Mvc;
using NetPinProc.Domain.MachineConfig;
using NetPinProc.Game.Manager.Server.Controllers.Base;

namespace NetPinProc.Game.Manager.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SteppersController : ConfigEntryControllerBase<StepperConfigFileEntry>
    {
        public SteppersController(
            ILogger<ConfigEntryControllerBase<StepperConfigFileEntry>> logger)
            : base(logger)
        {
        }
    }
}
