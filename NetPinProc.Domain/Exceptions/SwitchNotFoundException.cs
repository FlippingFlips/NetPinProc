namespace NetPinProc.Domain.Exceptions
{
    public class SwitchNotFoundException : MachineItemNotFoundException
    {
        public SwitchNotFoundException(string message) : base(message) { }
    }
}
