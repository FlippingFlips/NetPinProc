using NetPinProc.Domain;
using NetPinProc.Domain.PinProc;
using NetPinProc.Game;

namespace NetProcGame.SerialLeds
{
    internal class Game : BaseGameController
    {
        public Game(MachineType machineType, ILogger logger, bool Simulated = false, MachineConfiguration? configuration = null) :
            base(machineType, logger, Simulated, configuration) { } 
    }
}
