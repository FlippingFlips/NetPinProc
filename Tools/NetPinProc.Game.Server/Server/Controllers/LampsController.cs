﻿using Microsoft.AspNetCore.Mvc;
using NetPinProc.Domain.MachineConfig;
using NetPinProc.Game.Manager.Server.Controllers.Base;

namespace NetPinProc.Game.Manager.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LampsController : ConfigEntryControllerBase<LampConfigFileEntry>
    {
        public LampsController(
            ILogger<ConfigEntryControllerBase<LampConfigFileEntry>> logger) 
            : base(logger)
        {
        }
    }
}
