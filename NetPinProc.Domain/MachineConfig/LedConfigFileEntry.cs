using NetPinProc.Domain.Interface;

namespace NetPinProc.Domain.MachineConfig
{
    /// <summary>Represents an LED's configuration in memory</summary>
    public class LedConfigFileEntry  : ConfigFileEntryBase, IPolarity
    {
        ///<inheritdoc/>
        public string Number { get; set; }

        ///<inheritdoc/>
        public string Bus { get; set; }        

        ///<inheritdoc/>
        public string Tags { get; set; }

        ///<inheritdoc/>
        public bool Polarity { get; set; }
    }
}
