using NetPinProc.Domain;
using NetPinProc.Domain.Mode;
using NetPinProc.Domain.PinProc;
using NetPinProc.Game;

namespace NetPinProc.Game.Tests.GameTests.Fake
{
    public class FakeGameTests
    {
        [Fact]
        public async Task CreateFakeGameController_Tests()
        {
            var game = new FakeGame(MachineType.PDB, null);
            game.LoadConfig("Configs/machine.json");

            var tokenSource = new CancellationTokenSource();

            _ = Task.Run(() =>
            {
                game.Coils["trough"].Pulse(200);
                game.RunLoop(cancellationToken: tokenSource);
            });

            var mode = new Mode(game, 10);
            game.Modes.Add(mode);

            mode.AddSwitchHandler("trough1", SwitchHandleType.active, 0, new SwitchAcceptedHandler(OnSwitch));
            await Task.Delay(1000);

            var fakeDevice = game.PROC as IFakeProcDevice;
            fakeDevice?.AddSwitchEvent(32, EventType.SwitchClosedDebounced);

            await Task.Delay(25000);

            tokenSource.Cancel();
            await Task.Delay(1000);
        }

        private bool OnSwitch(Switch sw)
        {
            return true;
        }
    }

    public class FakeGame : GameController
    {
        public FakeGame(MachineType machineType, ILoggerPROC? logger = null, bool simulated = true) : base(machineType, logger, simulated)
        {
        }
    }
}
