using System;

namespace NetPinProc.Domain.Exceptions
{
    public class AdjustmentNotFoundException : Exception
    {
        public AdjustmentNotFoundException(string message) : base(message) { }
    }
}
