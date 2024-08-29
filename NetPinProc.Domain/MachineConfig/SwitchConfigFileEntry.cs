﻿using NetPinProc.Domain.PinProc;
using System.ComponentModel.DataAnnotations;

namespace NetPinProc.Domain.MachineConfig
{
    /// <summary>
    /// Represents the switch config entry in memory
    /// </summary>
    public class SwitchConfigFileEntry : ConfigFileEntryBase
    {
        /// <inheritdoc/>
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        /// <inheritdoc/>
        public SwitchType Type { get; set; }

        /// <inheritdoc/>
        public string Number { get; set; }

        /// <inheritdoc/>
        public string Tags { get; set; }

        /// <summary>Type of searchReset, open or closed</summary>
        public string SearchReset { get; set; }

        /// <summary>Type of searchStop, open or closed</summary>
        public string SearchStop { get; set; }

        /// <summary> This should be a comma separated 3 char colour. Wire can have two colors, just add one for single <para/>
        /// eg: YEL , or YEL,BLK
        /// </summary>
        public string InputWire { get; set; } = "YEL";

        /// <summary> This should be a comma separated 3 char colour. Wire can have two colors, just add one for single <para/>
        /// eg: YEL , or YEL,BLK
        /// </summary>
        public string GroundWire { get; set; } = "BLK";

    }
}
