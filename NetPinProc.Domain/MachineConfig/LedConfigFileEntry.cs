﻿using NetPinProc.Domain.Interface;

namespace NetPinProc.Domain.MachineConfig
{
    /// <summary>Represents an LED's configuration in memory</summary>
    public class LedConfigFileEntry  : ConfigFileEntryBase, IPolarity, IProcNumber, ILightInsert
    {
        ///<inheritdoc/>
        public string Number { get; set; }

        /// <summary>A number for reference if using long string address.<para/>
        /// The string number is the one decoded by PROC<para/>
        /// This is just for reference or to use in other applications</summary>
        public uint NumberPROC { get; set; }

        ///<inheritdoc/>
        public string Bus { get; set; }        

        ///<inheritdoc/>
        public string Tags { get; set; }

        ///<inheritdoc/>
        public bool Polarity { get; set; }

        public bool? SingleColor { get; set; }
        public double? ZRot { get; set; }
        public string ObjName { get; set; }
    }
}
