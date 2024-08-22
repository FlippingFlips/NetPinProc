using System;

namespace NetPinProc.Domain.Exceptions
{
    public class MachineItemNotFoundException : Exception
    {
        public MachineItemNotFoundException(string message) : base(message)
        {
        }
    }
}
