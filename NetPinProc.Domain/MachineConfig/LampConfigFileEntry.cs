using NetPinProc.Domain.Interface;

namespace NetPinProc.Domain.MachineConfig
{
    /// <summary>Represents a lamp's configuration in memory</summary>
    public class LampConfigFileEntry : ConfigFileEntryBase, IPolarity, IProcNumber, ILightInsert
    {
        ///<inheritdoc/>
        public string Number { get; set; }

        ///<inheritdoc/>
        public string Bus { get; set; }

        ///<inheritdoc/>
        public bool Polarity { get; set; } = false;

        ///<inheritdoc/>
        public string Tags { get; set; }

        public double? ZRot { get; set; }

        public string ObjName { get; set; }
    }
}
