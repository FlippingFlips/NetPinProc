#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;

namespace NetPinProc.Domain.Enums
{
    [Flags]
    public enum AuditType
    {
        Standard,
        Game,
        Earnings
    }
}
