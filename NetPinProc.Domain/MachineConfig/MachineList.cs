using System.Collections.Generic;

namespace NetPinProc.Domain.MachineConfig
{
    /// <summary>List with collection name</summary>
    /// <typeparam name="T"></typeparam>
    public class MachineList<T> : List<T>
    {
        public string CollectionName { get; set; }
    }
}
