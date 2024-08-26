using NetPinProc.Domain;
using NetPinProc.Game.Tests.Base;

namespace NetPinProc.Game.Tests.GameTests.P3_ROC.Integration
{
    /// <summary>
    /// 1.Create game (already created by the test base class), just use _game<para/>
    /// 2.Create Henrietta stepper mode and add to mode queue<para/>
    /// 3.Run teh game loop (RunGameLoop) (not awaiting it). Use CancelSource to stop the game<para/>
    /// 4.If set fake in consructor then to simulate a sent switch event - addswitchevent IFakePinProc<para/>
    /// </summary>
    public class LinearStepperMotorGameTests : GameContollerTestBase
    {
        /// <summary>Need this constructor to create a game for us</summary>
        /// <param name="fake">IsSimulated will be set</param>
        public LinearStepperMotorGameTests(bool fake = false) : base(fake) { }

        const int TIME = 32000000;

        /// <summary></summary>
        /// <param name="stepperName"></param>
        /// <param name="homeSwitchName"></param>
        /// <param name="homeDir"></param>
        /// <returns></returns>
        [Theory] [InlineData("Stepper", "stepperStop", 1)]
        public async Task StepperGoHome_Tests(
            string stepperName,
            string homeSwitchName,
            int homeDir)
        {
            //run the game dont wait
            this.RunGameLoop(1);

            // Create new mode and add to the game
            var hStepMode = new HenriettaLinearStepperMode(_game, 10, stepperName, homeDir);
            _game.Modes.Add(hStepMode);

            if(_isSimulated) 
            {
                await Task.Delay(2000);

                //if running fake simulated from the constructor,use the fake interface to send switch
                (_game.PROC as IFakeProcDevice)
                    .AddSwitchEvent(
                    _game.Switches[homeSwitchName].Number,
                    Domain.PinProc.EventType.SwitchClosedDebounced);

                await Task.Delay(1000);
                this.CancelSource.Cancel();
                Assert.True(true);
            }
            else
            {
                uint speed = 8500;

                //get a delay duration for home
                var delay = hStepMode.GoHomeHenrietta(speed);
                await Task.Delay(delay + 2000); //add a couple of seconds to make sure

                //make sure switch is closed
                Assert.True(hStepMode.Stepper.StopSwitch.IsClosed());

                delay = hStepMode.MoveLevelPlayfieldFromSwitch();
                await Task.Delay(delay + 500);
                
                //bring her out
                delay = hStepMode.RiseHenrietta(-22000);

                //spin on way up                
                await Task.Delay(delay + 1000);

                var spinDelay = hStepMode.Spin(1600 * 2, SpinSpeed.Slow);
                await Task.Delay(spinDelay / 2);

                spinDelay = hStepMode.Spin(1600 * 5, SpinSpeed.Fast);
                await Task.Delay(spinDelay + 1000);
            }
        }
    }
}
