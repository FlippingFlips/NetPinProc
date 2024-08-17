namespace NetPinProc.Game.Sqlite.Model
{
    /// <summary>
    /// Holds stats for a ball played
    /// </summary>
    public class BallPlayed
    {
        ///<inheritdoc/>
        public int Id { get; set; }
        ///<inheritdoc/>
        public int PlayerId { get; set; }
        ///<inheritdoc/>
        public int ScoreId { get; set; }
        /// <summary>
        /// Ball Number of game
        /// </summary>
        public byte Ball { get; set; }
        /// <summary>
        /// Points scored this ball
        /// </summary>
        public long Points { get; set; }
        /// <summary>
        /// Time in this ball
        /// </summary>
        public double Time { get; set; }
        ///<inheritdoc/>
        public Player Player { get; set; }
        ///<inheritdoc/>
        public Score Score { get; set; }
    }
}
