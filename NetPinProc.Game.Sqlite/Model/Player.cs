namespace NetPinProc.Game.Sqlite.Model
{
    /// <summary>Base player</summary>
    public class Player
    {
        /// <inheritdoc/>
        public int Id { get; set; }

        /// <inheritdoc/>
        public string Initials { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <summary>Default player on the machine</summary>
        public bool Default { get; set; }

        /// <summary>Data column for custom json etc</summary>
        public string Data { get; set; }
    }
}
