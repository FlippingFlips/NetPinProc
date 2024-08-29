namespace NetPinProc.Domain.Exceptions
{
    public class DriverNotFoundException : MachineItemNotFoundException
    {
        public DriverNotFoundException(string message) : base(message) { }
    }
}
