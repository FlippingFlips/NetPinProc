using NetPinProc.Domain.MachineConfig;

namespace NetPinProc.Game.Manager.Shared
{
    public class Machine : GameConfigFileEntry
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Version { get; set; }
    }
}
