using System;

namespace NetPinProc.Domain.Pdb
{
    /// <summary>PDLEd board stepper <para/>
    /// General stepper motor notes:<para/>
    ///    - Full step motors require 200 steps (1.8 degrees step angle), half step motors 400 steps (0.9 degrees)<para/>
    ///    </summary>
    public class PdStepper
    {
        /// <inheritdoc/>
        public readonly uint BoardAddress;

        const int TIME = 32000000;
        private readonly IPDLED board;
        /// <summary>Sets up a stepper and registering the speed</summary>
        /// <param name="proc"></param>
        /// <param name="name"></param>
        /// <param name="boardId"></param>
        /// <param name="stepperIndex"></param>
        /// <param name="speed"></param>
        /// <param name="stopSw"></param>
        public PdStepper(IProcDevice proc,
            string name,
            uint boardId,
            byte stepperIndex,
            uint speed,
            Switch stopSw = null)
        {
            Name = name;
            BoardAddress = boardId;
            StepperIndex = stepperIndex;
            StopSwitch = stopSw;
            
            //get the board this led address uses
            var pdLedBoard = PdLeds.GetPdLedBoard(BoardAddress);

            //assign the board to the led if not create one under this address
            if (pdLedBoard != null) { board = pdLedBoard; }
            else
            {
                this.board = new PDLED(proc, BoardAddress);
                PdLeds.PDLEDS.Add(this.board);
            }

            SetSpeed(speed);
        }

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public uint Speed { get; private set; }

        /// <inheritdoc/>
        public byte StepperIndex { get; }

        /// <summary></summary>
        public Switch StopSwitch { get; set; }

        /// <summary>Gets a duration for the movement position and speed set on the stepper<para/>
        /// Don't know how accurate or is the working the same as proc_devices.py.. MPF move_rel_pos</summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public int GetDuration(int pos)
        {
            var totalMs = TimeSpan
                    .FromMilliseconds(((Math.Abs(pos) * 2 * Speed) / TIME) + 0.3)
                    .TotalMilliseconds;

            return (int)(totalMs * 1000);
        }

        /// <summary></summary>
        /// <param name="pos"></param>
        public virtual void Move(int pos)
        {
            var sIndex = 23 + StepperIndex;
            if (pos > 0)
                board.WriteConfigRegister((uint)sIndex, (uint)pos);
            else
            {
                board.WriteConfigRegister((uint)sIndex, (uint)(System.Math.Abs(pos) + (1 << 15)));
            }
        }

        public virtual int MoveReturnDurationMs(int pos)
        {
            var duration = GetDuration(pos);
            Move(pos);
            return duration;
        }

        /// <summary>register the stepper speed = Stepper Ticks Per Half Period <para/></summary>
        /// <param name="speed"></param>
        public virtual void SetSpeed(uint speed)
        {
            Speed = speed;

            board.WriteConfigRegister(22, Speed);
        }

        /// <summary>Sets zero on the config register for this stepper</summary>
        public virtual void Stop() => board.WriteConfigRegister((uint)23 + StepperIndex, 0);
    }
}
