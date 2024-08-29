namespace NetPinProc.Domain.Exceptions
{
    public class LedNotFoundException : MachineItemNotFoundException
    {
        public LedNotFoundException(string message) : base(message) { }
    }
}
