namespace NetPinProc.Game.Sqlite.Model
{
    /// <summary>
    /// Color set to map short name colors to a color to be used for wires in UI or a manual
    /// </summary>
    public class ColorSet
    {
        /// <summary> 3 Char name of colour BLK, BLU, WHT </summary>
        public string Name { get; set; }

        /// <summary> RGB html color </summary>
        public string HtmlCode { get; set; }
    }
}
