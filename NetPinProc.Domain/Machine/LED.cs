using NetPinProc.Domain.Pdb;
using System.Collections.Generic;
using System.Linq;

namespace NetPinProc.Domain
{
    /// <summary>Represents an LED LED in a pinball machine</summary>
    public class LED : GameItem
    {
        private readonly List<uint> addrs;
        private readonly uint boardAddress;
        private LEDScript[] _activeScript;
        private double _lastTimeChanged;
        private double _nextActionTime;
        private bool _scriptFinished;
        private int _scriptIndex;
        private bool _scriptRepeat;
        private int _scriptRuntime;
        private double _scriptStartTime;
        private readonly IPDLED board;
        private string function = "none";
        private double lastTimeChanged;

        /// <summary>Creates an LED from the given strNumber <para/>
        /// Board address then colors A0-R0-G1-B2 <para/>
        /// Single number addresses can be used for 3 color like 0, or 45 etc <para/></summary>
        /// <param name="proc"></param>
        /// <param name="name"></param>
        /// <param name="number"></param>
        /// <param name="strNumber"></param>
        /// <param name="singleColor">currently only used if supplying a single number string</param>
        public LED(IProcDevice proc,
            string name,
            ushort number,
            string strNumber = "",
            bool singleColor = false) : 
            base(proc, name, number, strNumber)
        {
            //get the board index and color addresses
            //single number address
            if (uint.TryParse(strNumber, out var _ledNumber))
                addrs = PDBFunctions.PdLedRGBAddress(_ledNumber, singleColor);
            //get board index and colors from the string A0-R0-G1-B2
            else
                addrs = PDBFunctions.PdLedRGBAddress(strNumber);

            //get the board this led address uses
            boardAddress = addrs[0];
            var pdLedBoard = PdLeds.GetPdLedBoard(boardAddress);

            //assign the board to the led if not create one under this address
            if (pdLedBoard != null) { board = pdLedBoard; }
            else
            {
                this.board = new PDLED(proc, boardAddress);
                PdLeds.PDLEDS.Add(board);
            }

            //remove the board address from addr
            addrs.Remove(0);
        }

        /// <summary>The last color set from this class before sending to board</summary>
        public uint[] CurrentColor { get; private set; } = new uint[] { 0x00, 0x00, 0x00 };

        /// <summary>Reverse color polarity?</summary>
        public bool Polarity { get; set; } = false;

        /// <summary>Writes a new color using the <see cref="PDLED.WriteColor(uint, uint)"/></summary>
        /// <param name="color"></param>
        public void ChangeColor(uint[] color)
        {
            for (int i = 0; i < 3; i++)
            {
                var newColor = NormalizeColor(color[i]);
                CurrentColor[i] = newColor;
                board.WriteColor(addrs[i], newColor);
            }
        }

        /// <summary>Change color and clear any scripts. <para/>
        /// You need to use 0xFF for 255, not sure why...<para/>
        /// To test 255,255,0 for yellow it will be red, so you would need to do 0xFF,0xFF,0xFF<para/></summary>
        /// <param name="color"></param>
        public void Color(uint[] color)
        {
            function = "none";
            ChangeColor(color);
        }

        /// <summary>Turns off the LED and functions</summary>
        public void Disable()
        {
            function = "none";
            for (int i = 0; i < 3; i++)
            {
                var newColor = NormalizeColor(0);
                CurrentColor[i] = newColor;
                board.WriteColor(addrs[i], newColor);
            }
            lastTimeChanged = Time.GetTime();
        }

        /// <summary>Fades the LED disabling any scripts</summary>
        /// <param name="color"></param>
        /// <param name="time"></param>
        public void Fade(uint[] color, uint time)
        {
            function = "none";
            ChangeFade(color, time);
        }

        /// <summary>The sum of all durations in the script</summary>
        /// <param name="ledScript"></param>
        /// <returns></returns>
        public long GetScriptDuration(LEDScript[] ledScript) => ledScript.Sum(x => x.Duration);

        /// <summary></summary>
        public void Script(LEDScript[] newScript, int runtime = 0, bool repeat = true)
        {
            _scriptIndex = -1;
            _activeScript = newScript;
            _scriptRuntime = runtime;
            function = "script";
            _scriptStartTime = Time.GetTime();
            _scriptFinished = false;
            _scriptRepeat = repeat;
            IterateScript();
        }

        /// <summary>Processes any script function, if script ready then <see cref="IterateScript"/></summary>
        public void Tick()
        {
            if (function == "script")
            {
                var time = Time.GetTimeMs();
                if ((_scriptRuntime == 0 && !_scriptFinished) || (time - _scriptStartTime) < _scriptRuntime)
                {
                    if (time >= _nextActionTime)
                        IterateScript();
                }
                else
                    function = "none";
            }
        }

        /// <summary>Displays Name, Address and color_addresses</summary>
        /// <returns>led name, board address, color indexes</returns>
        public override string ToString() => $"LED: {Name}, board_addr: {boardAddress}, color_addrs: {string.Join(",", addrs)}";

        private void ChangeFade(uint[] color, uint time)
        {
            board.WriteFadeTime(time * 0x100);
            for (int i = 0; i < 3; i++)
            {
                var newColor = NormalizeColor(color[i]);
                CurrentColor[i] = newColor;
                board.WriteFadeColor(addrs[i], newColor);
            }
        }
        private void IterateScript()
        {
            if (_scriptIndex == _activeScript.Length - 1) _scriptIndex = 0;
            else _scriptIndex++;

            //change the color or fade it
            var entry = _activeScript[_scriptIndex];
            if (entry.FadeTime == 0)
                ChangeColor(entry.Colour);
            else
                ChangeFade(entry.Colour, entry.FadeTime);

            _lastTimeChanged = Time.GetTime();
            _nextActionTime = Time.GetTimeMs() + entry.Duration;

            if (_scriptIndex == _activeScript.Length - 1)
            {
                if (!_scriptRepeat)
                    _scriptFinished = true;
            }
        }
        private uint NormalizeColor(uint color)
        {
            if (this.Polarity) return 0xFF - color;
            else return color;
        }
    }
}
