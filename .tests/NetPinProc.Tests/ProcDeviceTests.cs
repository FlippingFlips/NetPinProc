using NetPinProc;
using NetPinProc.Domain;
using NetPinProc.Domain.PinProc;
using System;
using Xunit;

namespace NetPinProc.Tests
{
    /// <summary>
    /// Integration testing a connected P3-PROC. <para/>
    /// </summary>
    public class ProcDeviceTests
    {
        const MachineType MACHINE_TYPE =
            MachineType.PDB;

        const byte PROC_OUTPUT_MODULE = 0;
        const int PROC_PDB_BUS_ADDRESS = 0xC00;

        public ProcDeviceTests()
        {

        }

        /// <summary>
        /// Integration testing
        /// </summary>
        [Fact]
        public void InitPROCDevice_Tests()
        {
            IProcDevice proc = null;
            try
            {
                proc = new ProcDevice(MACHINE_TYPE);
                System.Threading.Thread.Sleep(100);
                proc.Reset(1);

                //load machine config.
                var config = MachineConfiguration.FromFile("machine.json");
                proc.SetupProcMachine(config);

                var boardAddress = 0;
                uint addr = 0;
                var baseRegAddress = 0x01000000 | (boardAddress & 0xA3) << 16;
                uint data = (uint)baseRegAddress | addr % 0xFF;
                var result = proc.ReadData(PROC_OUTPUT_MODULE, PROC_PDB_BUS_ADDRESS, ref data);

                proc?.Close();

                Assert.True(true); //got this far to pass as device is initialized in ProcDevice constructor
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
