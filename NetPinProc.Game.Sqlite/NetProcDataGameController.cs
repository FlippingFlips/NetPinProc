using NetPinProc.Game.Sqlite.Model;
using NetPinProc.Domain;
using NetPinProc.Domain.PinProc;
using NetPinProc.Game;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetPinProc.Game.Sqlite
{
    /// <summary>
    /// Game controller using <see cref="BasicGameController"/> with an instance to the database <para/>
    /// </summary>
    public class NetProcDataGameController : BasicGameController
    {
        /// <summary> Sqlite Entity Framework Database Context </summary>
        public static readonly INetProcDbContext Database = new NetProcDbContext();

        private readonly bool deleteOnInit;
        private bool _dbInitialized;

        private List<Player> _dbPlayers = new List<Player>();
        GamePlayed _gamePlayed = new GamePlayed();

        /// <summary>
        /// Database initialized when constructed. Machine Configuration is set from the database
        /// </summary>
        /// <param name="machineType"></param>
        /// <param name="deleteOnInit">delete the database when game is run?</param>
        /// <param name="logger"></param>
        /// <param name="simulated"></param>
        /// <param name="configuration"></param>
        public NetProcDataGameController(MachineType machineType, bool deleteOnInit, ILogger logger = null, bool simulated = false, MachineConfiguration configuration = null) 
            : base(machineType, logger, simulated, configuration)
        {
            this.deleteOnInit = deleteOnInit;

            InitializeDatabase();

            //create config from database entries
            _config = Database.GetMachineConfiguration();

            //set up machine from config
            LoadConfig(_config);
        }

        /// <summary>
        /// <inheritdoc/>. Adds TOTAL_BALLS_PLAYED to database
        /// </summary>
        public override void BallEnded()
        {
            base.BallEnded();
            IncrementAudit("TOTAL_BALLS_PLAYED", 1);
        }

        /// <summary>
        /// <inheritdoc/>. Adds and saves to the database if the name doesn't already exist.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override IPlayer CreatePlayer(string name)
        {
            var dbPlayer = Database.Players.FirstOrDefault(p => p.Name == name);
            if (dbPlayer == null)
            {
                dbPlayer = new Player() { Name = name, Initials = "" };
                Database.Players.Add(dbPlayer);
                Database.SaveChanges();
                Logger.Log(nameof(NetProcDataGameController) + $":created new database player.", LogLevel.Debug);
            }
            _dbPlayers.Add(dbPlayer);

            var player = new PlayerDb(name, dbPlayer.Id);
            return player;
        }

        /// <summary>
        /// <inheritdoc/>. Saves the game to the database. <see cref="SaveGamePlayed"/>
        /// </summary>
        public override void GameEnded()
        {
            base.GameEnded();
            SaveGamePlayed();
        }

        /// <summary>
        /// <inheritdoc/> <para/>
        /// Sets GAMES_STARTED and sets the <see cref="_gamePlayed"/> start time to now
        /// </summary>
        public override void GameStarted()
        {
            base.GameStarted();
            _gamePlayed.Started = DateTime.Now;
            IncrementAudit("GAMES_STARTED", 1);
        }

        /// <summary>
        /// Gets an adjustment value saved in database from lookup
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int GetAdjustment(string key) => Database.GetAdjustmentValue(key);

        /// <summary>
        /// Gets an audit value saved in database from lookup
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int GetAudit(string key) => Database.GetAuditValue(key);
        /// <summary>
        /// Tops scores from database
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Score> GetTopScores() => Database.ScoresTop();

        /// <summary>
        /// adds an audit value. use - values to decrement
        /// </summary>
        /// <param name="key"></param>
        /// <param name="increment"></param>
        public void IncrementAudit(string key, int increment) => Database?.IncrementAuditValue(key, increment);

        /// <summary>
        /// Initializes a p-roc Sqlite database and .db file if it doesn't exist. Runs any migrations <para/>
        /// </summary>
        public virtual void InitializeDatabase()
        {
            if (_dbInitialized) return;
            try
            {
                Database.InitializeDatabase(_machineType ==  MachineType.PDB ? true : false, deleteOnInit);
                IncrementAudit("POWERED_ON_TIMES", 1);
            }
            catch (System.Exception ex)
            {
                Logger?.Log($" NetProcDatabaseNode failed to initialize database. {ex.Message}-{ex.InnerException?.Message}", LogLevel.Error);
                throw;
            }

            _dbInitialized = true;
        }

        /// <summary>
        /// Saves database and disposes database
        /// </summary>
        public virtual void Quit()
        {
            SaveData();
            Database?.Dispose();
        }

        /// <summary>
        /// Saves changes to the database
        /// </summary>
        public virtual void SaveData() => Database?.SaveChanges();

        /// <summary>
        /// Adds GAMES_PLAYED to the database. First it will save a GamePlayed, then collect stats for every players ball, add the scores and then add balls played. <para/>
        /// Balls played consists of points, time, ball number
        /// </summary>
        public virtual void SaveGamePlayed()
        {
            Logger.Log(nameof(NetProcDataGameController) + $": saving last game, scores and balls played,", LogLevel.Error);

            _gamePlayed.BallsPerGame = BallsPerGame;
            _gamePlayed.Ended = DateTime.Now;
            IncrementAudit("GAMES_PLAYED", 1);

            //get game time for all players combined            
            double totalTime = 0;
            for (int i = 0; i < Players.Count; i++) totalTime += GetGameTime(i);
            _gamePlayed.GameTime = Math.Round(totalTime, 4);
            Database.GamesPlayed.Add(_gamePlayed);
            SaveData();

            //each of our saved players has ball stats, add these to a ball played and the total score
            foreach (PlayerDb p in Players)
            {
                var score = new Score()
                {
                    Points = p.BallsPlayed.Sum(x=>x.Points), //sum all the previous balls points
                    PlayerId = p.Id,
                    ExtraBallsPlayed = 0,
                    GamePlayedId = _gamePlayed.Id
                    //ExtraBallsPlayed //TODO
                };

                //add a ball played from the players ball stats
                p.BallsPlayed.ForEach(x =>
                {
                    Database.BallsPlayed.Add(new BallPlayed
                    {
                        Ball = x.Ball,
                        Time = Math.Round(x.Time, 4),
                        PlayerId = p.Id,
                        Points = x.Points,
                        Score = score
                    });
                });

                Database.Scores.Add(score);
            }

            try
            {
                SaveData();
            }
            catch (Exception ex)
            {
                Logger.Log(nameof(NetProcDataGameController) + $" database failed to save game played: {ex.Message} - {ex.InnerException?.Message}", LogLevel.Error);
            }
            _dbPlayers.Clear();
            _gamePlayed = new GamePlayed();
        }

        /// <summary>
        /// Sets adjustment value in database
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetAdjustment(string key, int value) => Database.SetAdjustmentValue(key, value);

        /// <summary>
        /// Sets audit value in database
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetAudit(string key, int value) => Database.SetAuditValue(key, value);
    }

    public class PlayerDb : Domain.Players.Player
    {
        public PlayerDb(string name, int id = 0) : base(name)
        {
            Id = id;
            BallsPlayed = new List<BallPlayed>();
        }

        public List<BallPlayed> BallsPlayed { get; set; }

        /// <summary>
        /// database id
        /// </summary>
        public int Id { get; }
    }
}
