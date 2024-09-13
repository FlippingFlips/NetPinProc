using NetPinProc.Domain.Interface;
using NetPinProc.Domain.PinProc;
using System.ComponentModel.DataAnnotations;

namespace NetPinProc.Domain.MachineConfig
{
    /// <summary>Represents the switch config entry in memory<para/>
    /// TODO: needs a de-bounce option?</summary>
    public class SwitchConfigFileEntry : ConfigFileEntryBase, IProcNumber
    {
        /// <inheritdoc/>
        public SwitchType Type { get; set; }

        /// <inheritdoc/>
        public string Number { get; set; }

        /// <summary>de-bounced switch rules are set by default, set to non debounce</summary>
        public bool? NonDebounce { get; set; }

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
