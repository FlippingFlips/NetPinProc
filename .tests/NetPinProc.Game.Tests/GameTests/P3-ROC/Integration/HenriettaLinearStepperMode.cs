using NetPinProc.Domain;
using NetPinProc.Domain.Exceptions;
using NetPinProc.Domain.Mode;
using NetPinProc.Domain.Pdb;

namespace NetPinProc.Game.Tests.GameTests.P3_ROC.Integration
{
    /// <summary>Custom mode for controlling a toy with linear stepper motor lift</summary>
    internal class HenriettaLinearStepperMode : LinearPdLedStepperMode
    {
        const int OFFSET_LVL = (int)(200 * 2.5);
        private readonly PdStepper _spinStepper;

        public HenriettaLinearStepperMode(
            IGameController game,
            int priority,
            string stepperName,
            int homeDir = 1) : 
            base(game, priority, stepperName, homeDir) 
        {
            _spinStepper = game.Steppers["Stepper2"];
            if (_spinStepper == null)
                throw new MachineItemNotFoundException<PdStepper>("no 2nd spinner found");
        }

        /// <summary>Moves the toy down to the switch home</summary>        
        /// <param name="speed"></param>
        /// <param name="steps">default to 150mm max</param>
        /// <returns>a delay in ms in how long it takes her to go home<para/>
        /// 0 if already at home </returns>
        public int GoHomeHenrietta(
            uint speed = 8500,
            uint steps = (200 * 150))
        {
            if (Stepper.StopSwitch.IsClosed())
                return 0;

            return StepperGoHome(speed, steps);
        }

        /// <summary>1600 full turn</summary>
        /// <param name="steps"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public int Spin(int steps, SpinSpeed speed = SpinSpeed.Medium)
        {
            _spinStepper.Stop();

            if (speed == SpinSpeed.None) return 0;

            _spinStepper.SetSpeed((uint)speed);

            return _spinStepper?.MoveReturnDurationMs(steps) ?? 0;
        }

        /// <summary>Moves the toy out of playfield and level with playfield</summary>
        /// <returns></returns>
        public int RiseHenrietta(int steps = -(200 * 110)) => 
            StepperMove(8500, steps);

        /// <summary>Calls the base to stop the stepper <para/>
        /// Then moves up a few mm (OFFSET_LVL) to playfield</summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public override bool StepperHomeSwitchClosed(Switch sw)
        {
            bool switchContinue = base.StepperHomeSwitchClosed(sw);            

            _spinStepper.Stop();

            return switchContinue;
        }

        public int MoveLevelPlayfieldFromSwitch() => StepperMove(8500, -OFFSET_LVL);
    }


    public enum SpinSpeed
    {
        None = 0,
        Slow = 26500,
        Medium = Slow / 2,
        Fast = Slow / 3,
        Rapid = Slow / 4,
    }
}
