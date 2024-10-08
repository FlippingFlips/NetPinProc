﻿using NetPinProc.Domain.Interface;

namespace NetPinProc.Domain.MachineConfig
{
    /// <summary>Machine config for Steppers on PDLEd boards</summary>
    public class StepperConfigFileEntry : ConfigFileEntryBase, IPdbBoard
    {
        /// <summary> Stepper 0 or 1 </summary>
        public bool IsStepper1 { get; set; }

        ///<inheritdoc/>
        public byte BoardId { get; set; }

        /// <summary> Stepper speed </summary>
        public uint Speed { get; set; }

        /// <summary> Flag to enable the servo read by config before registering to proc</summary>
        public bool IsEnabled { get; set; } = true;

        /// <summary> Stop Home / End switch</summary>
        public string StopSwitch { get; set; }

        /// <summary> Voltage of the stepper motor</summary>
        public byte? Voltage { get; set; } = 12;
    }
}
