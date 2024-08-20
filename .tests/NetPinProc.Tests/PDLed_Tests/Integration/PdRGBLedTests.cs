using NetPinProc.Domain;
using NetPinProc.Domain.Pdb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NetPinProc.Tests.PDLed_Tests.Integration
{
    /// <summary>Test RGBs on PD LED boards for P3ROC-PDB</summary>
    public class PdRGBLedTests : ProcDeviceTestBase
    {
        readonly AttrCollection<ushort, string, LED> _leds = new();

        [Theory]
        [InlineData(10000)]
        public async Task ScriptRedGreenBlue_ToAllLEDS_For10secs_Tests(
            int stopTestDelay)
        {
            try
            {
                //load machine config.
                MachineConfiguration config = LoadMachineConfigFile();

                //make sure we have leds to control
                Assert.True(config.PRLeds?.Any() ?? false);

                //create a device then reset the board
                InitPRCODeviceAndReset();

                PROC.SetupProcMachine(config, _leds: _leds);

                //make sure the setup has added us some leds
                Assert.True(_leds.Count > 0);

                foreach (var led in _leds)
                {
                    //led.Value.Script(script);

                    //GBR - green, blue, red
                    // - Polarity true
                    // - These are solid colors but more transparent when polarity is set to false. 
                    led.Value.Color(new uint[] { 0xFF, 0, 0 }); //GREEN
                    //led.Value.Color(new uint[] { 0, 0xFF, 0 }); //BLUE
                    //led.Value.Color(new uint[] { 0, 0, 0xFF }); //RED
                    //led.Value.Color(new uint[] { 0xFF, 0xFF, 0 }); //YELLOW                
                }

                //close this test on delay and stop the stepper
                await Task.Delay(stopTestDelay);

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
