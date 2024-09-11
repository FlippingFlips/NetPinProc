namespace NetPinProc.Game.Manager.Shared.Dto
{
    public class SvgDto
    {
        /// <summary>Svg xml string raw</summary>
        public string? Svg { get; set; }

        /// <summary>Should be a full base64 url "data:image/gif;base64,8oo8ie$"</summary>
        public string? Image { get; set; }

        /// <summary>machine items to include in export</summary>
        public Dictionary<string, bool>? Include { get; set; }
    }
}
