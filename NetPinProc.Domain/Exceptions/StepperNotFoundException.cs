namespace NetPinProc.Domain.Exceptions
{
    public class StepperNotFoundException : MachineItemNotFoundException
    {
        public StepperNotFoundException(string message) : base(message) { }
    }
}
