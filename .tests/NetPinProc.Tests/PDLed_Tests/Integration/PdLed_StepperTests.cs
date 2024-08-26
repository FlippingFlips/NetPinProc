using System.Threading.Tasks;
using System;
using Xunit;
using NetPinProc.Domain;
using System.Linq;
using NetPinProc.Domain.Pdb;

namespace NetPinProc.Tests.PDLed_Tests.Integration
{
    public class PdLed_StepperTests : ProcDeviceTestBase
    {
        /// <summary>PROC.SetupProcMachine will assign the items from the machine.json</summary>
        readonly AttrCollection<ushort, string, PdStepper> _steppers = new();

        readonly AttrCollection<ushort, string, Switch> _switches = new();

        /// <summary>Clockwise stepper</summary>
        /// <param name="name"></param>
        /// <param name="speed"></param>
        /// <param name="move"></param>
        /// <returns></returns>
        [Theory]        
        [InlineData("Stepper2", 26500, 800*3)] //1600 full turn
        //[InlineData("Stepper2", 22500, 400)] //1600 full turn
        public async Task MoveStepperOnPDLEDBoard_400Microsteps_Tests(
            string name,
            uint speed,
            int move)
        {
            try
            {
                //load machine config.
                MachineConfiguration config = LoadMachineConfigFile();

                //make sure we have steppers to control
                Assert.True(config.PRSteppers?.Any() ?? false);

                //create a device then reset the board
                InitPRCODeviceAndReset();
                PROC.WatchDogTickle();

                PROC.SetupProcMachine(config, _steppers: _steppers, _switches: _switches);               

                //make sure the setup has added us some steppers
                Assert.True(_steppers.Count > 0);
                Assert.True(_steppers.ContainsKey(name));                

                PdStepper stepper = _steppers[name];
                stepper.Stop();
                PROC.WatchDogTickle();
                //await Task.Delay(2000);

                //we have valid stepper from config, set speed and move

                //stepper.Stop();
                stepper.SetSpeed(speed);
                PROC.WatchDogTickle();

                var delay = stepper.MoveReturnDurationMs(move);                

                //close this test on delay and stop the stepper
                await Task.Delay(delay);

                stepper.SetSpeed(speed/2);
                delay = stepper.MoveReturnDurationMs(move);
                await Task.Delay(delay);

                stepper.SetSpeed(speed / 3);
                delay = stepper.MoveReturnDurationMs(move);
                await Task.Delay(delay);

                stepper.SetSpeed(speed / 4);
                delay = stepper.MoveReturnDurationMs(move);
                await Task.Delay(delay);

                PROC.WatchDogTickle();

                //stepper.Stop();
                PROC?.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                PROC?.Close();
                throw;
            }
        }
    }
}
