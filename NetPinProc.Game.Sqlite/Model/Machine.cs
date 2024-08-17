using NetPinProc.Domain.MachineConfig;

namespace NetPinProc.Game.Sqlite.Model
{
    /// <summary>
    /// Machine information
    /// </summary>
    public class Machine : GameConfigFileEntry
    {
        /// <inheritdoc/>
        public int Id { get; set; }
        /// <inheritdoc/>
        public string Title { get; set; }
        /// <inheritdoc/>
        public string Version { get; set; }
    }
}
