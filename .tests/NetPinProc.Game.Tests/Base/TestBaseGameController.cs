﻿using NetPinProc.Domain;
using NetPinProc.Domain.PinProc;

namespace NetPinProc.Game.Tests.Base
{
    /// <summary>Base Procgame GAME controller</summary>
    public class TestBaseGameController : BasicGameController
    {
        public TestBaseGameController(MachineType machineType,
            ILoggerPROC? logger = null,
            bool simulated = false,
            MachineConfiguration? configuration = null) :
            base(machineType, logger, simulated, configuration)
        {
        }
    }
}