namespace NetPinProc.Domain.Exceptions
{
    public class LampNotFoundException : MachineItemNotFoundException
    {
        public LampNotFoundException(string message) : base(message) { }
    }
}
