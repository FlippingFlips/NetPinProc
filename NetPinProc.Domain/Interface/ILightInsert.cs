namespace NetPinProc.Domain.Interface
{
    /// <summary>For led / lamp inserts in 3D space</summary>
    public interface ILightInsert
    {
        /// <summary>object name, shape name</summary>
        double? ZRot { get; set; }

        /// <summary>object name, shape name. 19mm_Arrow</summary>
        string ObjName { get; set; }
    }
}
