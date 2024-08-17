#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;

namespace NetPinProc.Game.Sqlite.Model
{
    public class Audit
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public AuditType Type { get; set; }
    }

    [Flags]
    public enum AuditType
    {
        Standard,
        Game,
        Earnings
    }
}
