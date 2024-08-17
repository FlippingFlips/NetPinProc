#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetPinProc.Game.Sqlite.Model
{
    public class GamePlayed
    {
        public int Id { get; set; }
        public DateTime Started { get; set; }
        public DateTime Ended { get; set; }
        /// <summary>
        /// Serialize the adjustments into here so we have all adjustment set for the game
        /// </summary>
        public string Adjustments { get; set; }
        public byte BallsPerGame { get; set; }

        /// <summary>
        /// Total off all the GameTimes for the each player in seconds
        /// </summary>
        [Column(TypeName = "DOUBLE(18, 2)")]
        public double GameTime { get; set; }
        public ICollection<Score> Scores { get; set; }
    }
}
