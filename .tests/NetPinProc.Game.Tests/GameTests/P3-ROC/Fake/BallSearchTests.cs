using NetPinProc.Domain;
using NetPinProc.Domain.Mode;
using NetPinProc.Domain.PinProc;

namespace NetPinProc.Game.Tests.GameTests.Fake
{
    public class BallSearchTests
    {
        /// <summary>Setup a fake game and enable ball search</summary>
        /// <returns></returns>
        [Fact]
        public async Task BallSearch_Delay_PerfomSearch_Should_Callback_CompletedHandler_Tests()
        {
            //create a game using the machine.json
            var game = new FakeGame(MachineType.PDB, null);
            game.LoadConfig("Configs/machine.json");

            //set this variable of the handler worked
            bool _searchDelayResult = false;

            //create ball search mode and add it to the queue.
            //set up anon delay to call back and set the result
            var mode = new BallSearch(game, 3, null, completed_handler: new AnonDelayedHandler(() => _searchDelayResult = true));
            game.Modes.Add(mode);

            //run the game loop
            var tokenSource = new CancellationTokenSource();            
            _ = Task.Run(() =>
            {
                //game.Coils["trough"].Pulse(200);
                game.RunLoop(cancellationToken: tokenSource);
            });

            //enable the ball search, this will reset it and start the delay
            mode.Enable();

            //give delay for integration
            await Task.Delay(6000);
            Assert.True( _searchDelayResult );
            tokenSource.Cancel();            
        }
    }
}
