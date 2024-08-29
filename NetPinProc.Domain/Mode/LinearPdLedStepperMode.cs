using NetPinProc.Domain.Exceptions;
using NetPinProc.Domain.Pdb;
using NetPinProc.Domain.PinProc;

namespace NetPinProc.Domain.Mode
{
    /// <summary>linear stepper motor mode for lifts <para/>
    /// Create your own mode using this as a base and add to the modes queue</summary>
    public abstract class LinearPdLedStepperMode : Mode
    {
        private readonly int homeDirection;
        int position = 0;

        /// <summary></summary>
        /// <param name="game"></param>
        /// <param name="priority"></param>
        /// <param name="stepperName">the stepper name which should be in the Game.Steppers collection</param>
        /// <param name="homeDirection">Set a positive or negative direction for home</param>
        /// <exception cref="StepperNotFoundException"></exception>
        /// <exception cref="SwitchNotFoundException"></exception>
        public LinearPdLedStepperMode(IGameController game,
            int priority,
            string stepperName,
            int homeDirection = 1) : base(game, priority) 
        {
            if (!game.Steppers.ContainsKey(stepperName))
                throw new StepperNotFoundException(stepperName);

            Stepper = game.Steppers[stepperName];
            if (Stepper.StopSwitch == null)
                throw new SwitchNotFoundException(stepperName);

            this.AddSwitchHandler(Stepper.StopSwitch.Name, SwitchHandleType.closed, 0, StepperHomeSwitchClosed);
            this.homeDirection = homeDirection;
        }

        public PdStepper Stepper { get; }

        /// <summary>Moves the stepper by the given steps if the StopSwitch isn't closed</summary>
        /// <param name="speed">requires a speed</param>
        /// <param name="steps">How many steps to move home. <para/>
        /// The default vaule is to move 112mm (24000) for 200 steps per mm.</param>
        public virtual int StepperGoHome(uint speed, uint steps = 24000)
        {
            if (Stepper.StopSwitch.IsClosed())
            {
                position = 0;
                Game.Logger.Log($"{Stepper.Name} is at home position");
                return 0;
            }
            else
            {
                if (homeDirection > 0) return StepperMove(speed, (int)steps);
                else if (homeDirection < 0) return StepperMove(speed, -(int)steps);
                else { Game.Logger.Log(
                    LogLevel.Warning,
                    $"{nameof(StepperGoHome)}: can't go home because home direction is set to 0");
                    return 0;
                }
            }
        }

        /// <summary>Just invokes Stepper.Move</summary>
        /// <param name="speed">Only sets the speed if it's different to the current speed setting</param>
        /// <param name="amt">steps</param>
        public virtual int StepperMove(uint speed, int amt)
        {
            if(Stepper.Speed != speed) { Stepper.SetSpeed(speed); }

            return Stepper.MoveReturnDurationMs(amt);
        }

        public virtual bool StepperHomeSwitchClosed(Switch sw)
        {
            Stepper.Stop();
            Game.Logger.Log($"homing stepper switch: {sw.Name} closed. Stopping stepper...", LogLevel.Info);            
            position = 0;

            return SWITCH_CONTINUE;
        }
    }
}
