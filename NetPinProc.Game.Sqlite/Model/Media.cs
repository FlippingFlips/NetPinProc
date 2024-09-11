namespace NetPinProc.Game.Sqlite.Model
{
    /// <summary>Storing user files in the database</summary>
    public class Media
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MimeType { get; set; }
        public byte[] Data { get; set; }
        public long Size { get; set; }
        public string Tags { get; set; }
    }
}
