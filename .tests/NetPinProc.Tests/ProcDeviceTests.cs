using NetPinProc.Domain;
using NetPinProc.Domain.PinProc;
using System;
using System.Threading.Tasks;
using Xunit;

namespace NetPinProc.Tests
{

    /// <summary>Integration testing a connected P-ROC or P3-PROC boards.<para/>
    /// Change values in the machine.json to different machine types, switches etc<para/>
    /// These should be very basic tests to test connection, opening and closing</summary>
    public class ProcDeviceTests : ProcDeviceTestBase
    {
        readonly AttrCollection<ushort, string, IDriver> _coils = new();

        /// <summary>Requires a running PROC board connected to the USB. <para/>
        /// Creates a PROCDevice from the type set in the `machine.json`, close is called on the device when test ends
        /// </summary>
        [Fact]
        public void InitPROC_PDB_Device_Tests()
        {            
            try
            {
                //load machine config.
                MachineConfiguration config = LoadMachineConfigFile();

                //create a device then reset the board
                InitPRCODeviceAndReset();

                PROC.SetupProcMachine(config, _coils: _coils);

                var boardAddress = 0;
                uint addr = 0;
                var baseRegAddress = 0x01000000 | (boardAddress & 0xA3) << 16;
                uint data = (uint)baseRegAddress | addr % 0xFF;
                Result result = PROC.ReadData(PROC_OUTPUT_MODULE, PROC_PDB_BUS_ADDRESS, ref data);

                PROC?.Close();

                //got this far to pass as device is initialized in ProcDevice constructor
                Assert.True(result == Result.Success, "Read data from P-ROC successfully");
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
