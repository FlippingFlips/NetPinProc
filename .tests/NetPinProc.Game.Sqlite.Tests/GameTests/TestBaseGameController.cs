using NetPinProc.Domain;
using NetPinProc.Domain.PinProc;

namespace NetPinProc.Game.Sqlite.Tests.GameTests
{
    /// <summary>Test Game controller derived from a sqlite NetProcDataGameController</summary>
    public class TestBaseGameController : NetProcDataGameController
    {
        public TestBaseGameController(MachineType machineType,
            ILoggerPROC? logger = null,
            bool simulated = false) :
            base(machineType, false, logger, simulated)
        {
        }
    }
}