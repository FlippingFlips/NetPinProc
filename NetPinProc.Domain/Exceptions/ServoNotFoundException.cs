namespace NetPinProc.Domain.Exceptions
{
    public class ServoNotFoundException : MachineItemNotFoundException
    {
        public ServoNotFoundException(string message) : base(message) { }
    }
}
