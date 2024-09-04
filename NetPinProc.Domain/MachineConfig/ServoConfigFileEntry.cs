using NetPinProc.Domain.Interface;

namespace NetPinProc.Domain.MachineConfig
{
    /// <summary> Machine config for Servos on PDLEd boards, 12 per board indexed 0-11</summary>
    public class ServoConfigFileEntry : ConfigFileEntryBase, IPdbBoard
    {
        /// <summary> Servo index 0 - 11</summary>
        public uint Index { get; set; }

        ///<inheritdoc/>
        public byte BoardId { get; set; }

        /// <summary> Minimum servo value</summary>
        public int MinValue { get; set; }

        /// <summary> Flag to enable the servo read by config before registering to proc</summary>
        public bool IsEnabled { get; set; } = true;

        /// <summary> Voltage of the stepper motor</summary>
        public byte? Voltage { get; set; } = 5;
    }
}
