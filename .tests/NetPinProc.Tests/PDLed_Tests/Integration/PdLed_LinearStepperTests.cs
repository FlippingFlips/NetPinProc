using System.Threading.Tasks;
using System;
using Xunit;
using NetPinProc.Domain;
using System.Linq;
using NetPinProc.Domain.Pdb;

namespace NetPinProc.Tests.PDLed_Tests.Integration
{
    public class PdLed_LinearStepperTests : ProcDeviceTestBase
    {
        /// <summary>PROC.SetupProcMachine will assign the items from the machine.json</summary>
        readonly AttrCollection<ushort, string, PdStepper> _steppers = new();

        readonly AttrCollection<ushort, string, Switch> _switches = new();

        /// <summary>dont move far in case jam for too long, she will burn</summary>
        /// <param name="name">name of stepper in config</param>
        /// <param name="speed">SLOW 10000, Faster 2500</param>
        /// <param name="move">move amount</param>
        /// <param name="stopTestDelay"></param>
        /// <returns></returns>
        [Theory]
        //[InlineData("Stepper", 8500, (200 * 1), 1500)] //Move Down 1mm
        //[InlineData("Stepper", 6500, (200 * 2), 1500)] //Move Down 2mm
        //[InlineData("Stepper", 6500, (200 * 50), 7000)] //Move Down 50mm
        //[InlineData("Stepper", 6500, (200 * 90), 15000)] //Move Down 90mm
        //
        //[InlineData("Stepper", 8500, -(200 * 1), 3000)] //Move Up 10mm
        //[InlineData("Stepper", 8500, -(200 * 20), 7000)] //Move Up 20mm
        //[InlineData("Stepper", 8500, -(200 * 50), 10000)] //Move Up 30mm
        //[InlineData("Stepper", 8500, -(200 * 153), 15000)] //Move Up 153mm
        //[InlineData("Stepper", 8500, -(200 * 2), 2000)] //Move Up 90mm
        //
        //[InlineData("Stepper", 6500, -(200 * 112), 12500)] //Move Up 109mm from bottom switch
        [InlineData("Stepper", 8500, (200 * 1), 4000)] //Move Up 5mm from very bottom
        //
        //[InlineData("Stepper", 6500, (200 * 150), 12500)] //Move Down 150mm to switch
        //[InlineData("Stepper", 6500, (200 * 153), 20000)] //FULL DISTANCE
        //[InlineData("Stepper", 6500, (200 * 153), 20000)] //Move Up 153mm
        public async Task MoveLinearStepperOnPDLEDBoard_400Microsteps_Tests(
            string name,
            uint speed,
            int move,
            int stopTestDelay)
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
                //stepper.Stop();
                //await Task.Delay(2000);

                //we have valid stepper from config, set speed and move

                //stepper.Stop();
                stepper.SetSpeed(speed);
                PROC.WatchDogTickle();

                stepper.Move(move);                

                //close this test on delay and stop the stepper
                await Task.Delay(stopTestDelay);

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
