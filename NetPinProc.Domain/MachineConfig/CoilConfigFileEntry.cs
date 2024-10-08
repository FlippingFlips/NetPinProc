﻿using NetPinProc.Domain.Interface;

namespace NetPinProc.Domain.MachineConfig
{

    /// <summary>Represents a coil (driver) config entry in memory</summary>
    public class CoilConfigFileEntry : ConfigFileEntryBase, IPolarity, IProcNumber
    {
        /// <summary> The number that you can reference in script </summary>
        public string Number { get; set; }

        /// <summary>A number for reference. The string number is the one decoded by PROC<para/>
        /// This is just for reference or to use in other applications</summary>
        public uint NumberPROC { get; set; }

        /// <summary>Default pulse time</summary>
        public int PulseTime { get; set; } = 30;

        /// <summary>Set to AuxPort</summary>
        public string Bus { get; set; }

        /// <summary>Defaults to true</summary>
        public bool Polarity { get; set; } = true;

        /// <summary>Tags for the coil for grouping etc</summary>
        public string Tags { get; set; }

        /// <summary> Pulse coil on ball search </summary>
        public int Search { get; set; }

        /// <summary> This should be a comma separated 3 char colour. Wire can have two colors, just add one for single <para/>
        /// eg: YEL , or YEL,BLK
        /// </summary>
        public string ReturnWire { get; set; } = "BLK";

        /// <summary> This should be a comma separated 3 char colour. Wire can have two colors, just add one for single <para/>
        /// eg: YEL , or YEL,BLK
        /// </summary>
        public string VoltageWire { get; set; } = "BLU VIO";

        /// <summary> Voltage amount of coil </summary>
        public byte? Voltage { get; set; } = 48;
    }
}
