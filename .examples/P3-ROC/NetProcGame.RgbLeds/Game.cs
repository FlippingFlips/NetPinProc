﻿using NetPinProc.Domain;
using NetPinProc.Domain.PinProc;
using NetPinProc.Game;

namespace NetProcGame.RgbLeds
{
    internal class Game : GameController
    {
        public Game(MachineType machineType, ILoggerPROC logger, bool Simulated = false) : base(machineType, logger, Simulated)
        {

        }

        internal void Setup()
        {
            LoadConfig(@"machine.json");
        }        
    }
}
