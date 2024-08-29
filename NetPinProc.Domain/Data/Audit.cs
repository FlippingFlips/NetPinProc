using NetPinProc.Domain.Enums;

namespace NetPinProc.Domain.Data
{
    public class Audit
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public AuditType Type { get; set; }
    }
}
