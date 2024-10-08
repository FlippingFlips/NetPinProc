﻿using Microsoft.AspNetCore.Mvc;
using NetPinProc.Domain.MachineConfig;
using NetPinProc.Game.Manager.Server.Controllers.Base;

namespace NetPinProc.Game.Manager.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriversController : ConfigEntryControllerBase<CoilConfigFileEntry>
    {
        public DriversController(
            ILogger<ConfigEntryControllerBase<CoilConfigFileEntry>> logger) 
            : base(logger)
        {
        }
    }
}
