namespace NetPinProc.Domain.Exceptions
{
    public class AdjustmentNotFoundException : MachineItemNotFoundException
    {
        public AdjustmentNotFoundException(string message) : base(message) { }
    }
}
