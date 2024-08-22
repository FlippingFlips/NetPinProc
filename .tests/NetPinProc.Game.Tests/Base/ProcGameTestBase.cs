﻿using NetPinProc.Domain;
using NetPinProc.Game.Tests.GameTests.Fake;

namespace NetPinProc.Game.Tests.Base
{
    public abstract class ProcGameTestBase
    {
        protected CancellationTokenSource CancelSource = new();
        protected MachineConfiguration MachineConfiguration;

        public ProcGameTestBase() => MachineConfiguration = LoadMachineConfigFile();

        protected MachineConfiguration LoadMachineConfigFile() =>
            MachineConfiguration.FromFile("Configs/machine.json");
    }

    public abstract class GameContollerTestBase : ProcGameTestBase
    {
        protected FakeGame _game;

        public GameContollerTestBase(bool fake = true)
        {
            _game = new FakeGame(MachineConfiguration.PRGame.MachineType, null, fake);
            _game.LoadConfig(MachineConfiguration);
        }

        /// <summary>Runs the games run loop</summary>
        /// <returns></returns>
        protected virtual async Task RunGameLoop(byte delayMs = 0) =>
            await Task.Run(() => _game.RunLoop(delayMs, CancelSource));
    }
}