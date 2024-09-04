namespace NetPinProc.Domain.MachineConfig
{
    /// <summary>Represents a GI string list element in memory</summary>
    public class GIConfigFileEntry : ConfigFileEntryBase
    {
        public string Number { get; set; }        
        public string Tags { get; set; }
    }
}
