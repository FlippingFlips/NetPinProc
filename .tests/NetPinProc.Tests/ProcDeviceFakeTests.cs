using NetPinProc.Domain;
using NetPinProc.Domain.PinProc;
using System;
using System.Threading.Tasks;
using Xunit;

namespace NetPinProc.Tests
{
    /// <summary>Fake ProcDevice Tests. <para/></summary>
    public class ProcDeviceFakeTests : ProcDeviceTestBase
    {
        /// <summary>The interface for controlling a p-roc board</summary>
        IProcDevice proc = null;

        /// <summary>Runs a fake P3-ROC board<para/>
        /// Creates an IProcDevice from a FakePinProc and sets up by machine type from the `machine.json`, close is called when ended
        /// </summary>
        [Fact]
        public async Task InitFakePROC_PDB_Device_Tests()
        {            
            try
            {
                //load machine config.
                var config = LoadMachineConfigFile();

                //create a device then reset the board
                proc = new FakePinProc(config.PRGame.MachineType, new ConsoleLogger());
                await Task.Delay(200);
                proc.Reset(1);
                
                proc.SetupProcMachine(config);

                var boardAddress = 0;
                uint addr = 0;
                var baseRegAddress = 0x01000000 | (boardAddress & 0xA3) << 16;
                uint data = (uint)baseRegAddress | addr % 0xFF;
                Result result = proc.ReadData(PROC_OUTPUT_MODULE, PROC_PDB_BUS_ADDRESS, ref data);

                proc?.Close();

                //got this far to pass as device is initialized in ProcDevice constructor
                Assert.True(result == Result.Success, "Read data from P-ROC successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                proc?.Close();
                throw;
            }
        }
    }
}
