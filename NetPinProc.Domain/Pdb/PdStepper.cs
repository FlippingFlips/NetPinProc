namespace NetPinProc.Domain.Pdb
{
    /// <summary>
    /// PDLEd board stepper
    /// </summary>
    public class PdStepper
    {
        /// <inheritdoc/>
        public readonly uint BoardAddress;
        private readonly IPDLED board;

        /// <summary>
        /// Sets up a stepper and registering the speed
        /// </summary>
        /// <param name="proc"></param>
        /// <param name="name"></param>
        /// <param name="boardId"></param>
        /// <param name="stepperIndex"></param>
        /// <param name="speed"></param>
        /// <param name="stopSw"></param>
        public PdStepper(IProcDevice proc, string name, uint boardId, byte stepperIndex, uint speed, string stopSw = null)
        {
            Name = name;
            BoardAddress = boardId;
            StepperIndex = stepperIndex;
            
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
        public void Move(int pos)
        {
            var sIndex = 23 + StepperIndex;
            if (pos > 0)
                board.WriteConfigRegister((uint)sIndex, (uint)pos);
            else
            {
                board.WriteConfigRegister((uint)sIndex, (uint)(System.Math.Abs(pos) + (1 << 15)));
            }
        }

        public void Stop() => board.WriteConfigRegister((uint)23 + StepperIndex, 0);

        /// <inheritdoc/>
        public byte StepperIndex { get; }

        /// <inheritdoc/>
        public uint Speed { get; private set; }

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <summary>
        /// register the stepper speed = Stepper Ticks Per Half Period <para/>
        /// Try speeds of 2500 for fast and higher values for slow
        /// </summary>
        /// <param name="speed"></param>
        public void SetSpeed(uint speed)
        {
            Speed = speed;

            board.WriteConfigRegister(22, Speed);
        }
    }
}
