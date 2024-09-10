using Microsoft.AspNetCore.Mvc;
using NetPinProc.Domain.MachineConfig;
using NetPinProc.Game.Manager.Server.Controllers.Base;

namespace NetPinProc.Game.Manager.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServosController : ConfigEntryControllerBase<ServoConfigFileEntry>
    {
        public ServosController(
            ILogger<ConfigEntryControllerBase<ServoConfigFileEntry>> logger) 
            : base(logger)
        {
        }
    }
}
