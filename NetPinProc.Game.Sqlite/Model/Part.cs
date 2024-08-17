namespace NetPinProc.Game.Sqlite.Model
{
    /// <summary>
    /// Parts table if needed. Might be useful one day for 2D exports of parts etc.
    /// </summary>
    public class Part
    {
        public string Name { get; set; }
        public string PartNo { get; set; }
        public double? XPos { get; set; }
        public double? YPos { get; set; }
        public double? ZPos { get; set; }
        public decimal? Cost { get; set; }
        public int? PartType { get; set; }
        public int? Material { get; set; }
        public string Shape { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
    }
}
