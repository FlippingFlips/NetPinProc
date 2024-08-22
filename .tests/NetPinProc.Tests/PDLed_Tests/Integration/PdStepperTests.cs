﻿using System.Threading.Tasks;
using System;
using Xunit;
using NetPinProc.Domain;
using System.Linq;
using NetPinProc.Domain.Pdb;

namespace NetPinProc.Tests.PDLed_Tests.Integration
{
    public class PdStepperTests : ProcDeviceTestBase
    {
        /// <summary>PROC.SetupProcMachine will assign the items from the machine.json</summary>
        readonly AttrCollection<ushort, string, PdStepper> _steppers = new();

        /// <summary>, dont move far in case jam for too long
        /// </summary>
        /// <param name="name">name of stepper in config</param>
        /// <param name="speed">SLOW 10000, Faster 2500</param>
        /// <param name="move">move amount</param>
        /// <param name="stopTestDelay"></param>
        /// <returns></returns>
        [Theory]
        //[InlineData("Stepper", 5000, -20000, 10000)]
        [InlineData("Stepper", 5000, 10000, 5000)] //Henrietta NEMA stepper - minus values moves up Playfield -47000
        public async Task MoveStepperOnPDLEDBoard_Tests(
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

                PROC.SetupProcMachine(config, _steppers: _steppers);

                //make sure the setup has added us some steppers
                Assert.True(_steppers.Count > 0);
                Assert.True(_steppers.ContainsKey(name));

                PROC.WatchDogTickle();

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