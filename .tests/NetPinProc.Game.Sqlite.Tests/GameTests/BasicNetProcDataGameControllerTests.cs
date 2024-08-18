using NetPinProc.Domain.PinProc;

namespace NetPinProc.Game.Sqlite.Tests.GameTests
{
    /// <summary></summary>
    public class BasicNetProcDataGameControllerTests
    {
        /// <summary>Create a fake pinproc?</summary>
        const bool SIMULATED = true;

        /// <summary>Testing a <see cref="NetProcDataGameController"/>. No game loop, just config. <para/>
        /// game and mode creating. Uses the `machine.json` for configuration.
        /// </summary>
        [Fact]
        public void CreateGameControllerAndConfig_PDB_Tests()
        {
            //create a new game. this game class will initiliaze the database
            var game = new NetProcDataGameController(MachineType.PDB, false, null, SIMULATED);

            //assert that we have machine items that have come from the database init scripts
            Assert.True(game.Switches?.Count > 0);
            Assert.True(game.LEDS?.Count > 0);
            Assert.True(game.Coils?.Count > 0);

            //reset to create trough add mode
            game.Reset();

            //test method on trough, it will be empty and show this isn't null
            Assert.True(game.Trough.IsFull() == false);
        }

        /// <summary>
        /// Create a simulated FAKE game controller with no machine config, that shouldn't crash but have zero machine items. <para/>
        /// This Game creates a trough so that should fail nicely and remind dev there is no config set
        /// </summary>
        [Fact]
        public void CreateGameControllerNoConfig_SwitchesEqualToZero_Tests()
        {
            //create game no machine config
            var game = new TestBaseGameController(MachineType.PDB, null, SIMULATED);

            //assert
            Assert.True(game.Switches.Count == 0);
        }
    }
}