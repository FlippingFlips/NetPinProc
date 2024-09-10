using System;

namespace NetPinProc.Domain.Exceptions
{
    public class MachineItemNotFoundException<T> : Exception
    {
        public MachineItemNotFoundException(string message) : base(message) { }

        public override string Message => base.Message;
    }
}
