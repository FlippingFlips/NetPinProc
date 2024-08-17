using System.Collections.Generic;
using System.Reflection;

namespace NetPinProc.Game.Sqlite.Model
{
    /// <summary>
    /// Database score
    /// </summary>
    public class Score
    {
        ///<inheritdoc/>
        public int Id { get; set; }
        ///<inheritdoc/>
        public long Points { get; set; }
        ///<inheritdoc/>
        public int PlayerId { get; set; }
        ///<inheritdoc/>
        public Player Player { get; set; }
        ///<inheritdoc/>
        public int GamePlayedId { get; set; }
        ///<inheritdoc/>
        public GamePlayed GamePlayed { get; set; }
        ///<inheritdoc/>
        public byte ExtraBallsPlayed { get; set; }
        /// <summary>
        /// Stats for each ball played
        /// </summary>
        public ICollection<BallPlayed> BallsPlayed { get; set; }
    }
}
