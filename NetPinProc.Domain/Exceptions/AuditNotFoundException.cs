using System;

namespace NetPinProc.Domain.Exceptions
{
    public class AuditNotFoundException : Exception
    {
        public AuditNotFoundException(string message) : base(message) { }
    }
}
