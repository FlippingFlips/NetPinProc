using NetPinProc.Domain;
using NetPinProc.Domain.PinProc;
using NetPinProc.Game;
using NetPinProc.Game.Modes;

namespace NetPinProc.Game.Sqlite.Tests
{
    public class GameControllerTests
    {
        /// <summary>
        /// path to machine configuration to set the board with
        /// </summary>
        const string MACHINE_JSON = "machine.json";

        /// <summary>
        /// Testing a simulated FAKE <see cref="BaseGameController"/>. No game loop, just config, game and mode creating. Uses the `machine.json` for configuration.
        /// </summary>
        [Fact]
        public void CreateGameControllerAndConfig_PDB_Tests()
        {
            //new method:
            //load config first and pass that to the game, the game can then setup the machine
            using var ctx = new NetProcDbContext();
            ctx.InitializeDatabase(true, true);
            var mc = ctx.GetMachineConfiguration();
            var game = new TestBaseGameController(MachineType.PDB, null, true, mc);

            //assert
            Assert.True(game.Switches?.Count > 0);
            Assert.True(game.LEDS?.Count > 0);
            Assert.True(game.Coils?.Count > 0);

            //reset to create trough add mode
            game.Reset();

            //test method on trough, it will be empty and show this isn't null
            Assert.True(game.Trough.IsFull() == false);
        }

        void ProcessEvents(IGameController game, Event[] events) { for (int i = 0; i < events?.Length; i++) { game.ProcessEvent(events[i]); } }

        /// <summary>
        /// Create a simulated FAKE game controller with no machine config, that shouldn't crash but have zero machine items. <para/>
        /// This Game creates a trough so that should fail nicely and remind dev there is no config set
        /// </summary>
        [Fact]
        public void CreateGameControllerNoConfig_SwitchesEqualToZero_Tests()
        {
            //create game no machine config
            var game = new TestBaseGameController(MachineType.PDB, null, true, null);

            //assert
            Assert.True(game.Switches.Count == 0);
        }
    }
}