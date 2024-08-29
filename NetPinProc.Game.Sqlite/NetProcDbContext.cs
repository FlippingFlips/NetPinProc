using Microsoft.EntityFrameworkCore;
using NetPinProc.Domain;
using NetPinProc.Domain.MachineConfig;
using NetPinProc.Game.Sqlite.Config;
using NetPinProc.Game.Sqlite.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NetPinProc.Game.Sqlite
{
    /// <summary>
    /// Connection string default <see cref="CONNECTION_STRING"/>. <para/>
    /// <see cref="InitializeDatabase"/> should be invoked to populate initial database and data from sql files
    /// </summary>
    public class NetProcDbContext : DbContext, INetProcDbContext
    {
        /// <summary>
        /// Database location
        /// </summary>
        public const string CONNECTION_STRING = "Data Source=./netproc.db";

        /// <summary>Entity Framework context</summary>
        public NetProcDbContext() { }

        /// <summary>Entity Framework context</summary>
        public NetProcDbContext(DbContextOptions<NetProcDbContext> dbContextOptions) : base(dbContextOptions) { }

        /// <inheritdoc/>
        public DbSet<Adjustment> Adjustments { get; set; }
        /// <inheritdoc/>
        public DbSet<Audit> Audits { get; set; }
        /// <inheritdoc/>
        public DbSet<BallPlayed> BallsPlayed { get; set; }
        /// <inheritdoc/>
        public DbSet<CoilConfigFileEntry> Coils { get; set; }
        /// <inheritdoc/>
        public DbSet<ColorSet> Colors { get; set; }
        /// <inheritdoc/>
        public DbSet<GamePlayed> GamesPlayed { get; set; }
        /// <inheritdoc/>
        public DbSet<LampConfigFileEntry> Lamps { get; set; }
        /// <inheritdoc/>
        public DbSet<LedConfigFileEntry> Leds { get; set; }
        /// <inheritdoc/>
        public DbSet<Machine> Machine { get; set; }
        /// <inheritdoc/>
        public DbSet<Part> Parts { get; set; }
        /// <inheritdoc/>
        public DbSet<Player> Players { get; set; }
        /// <inheritdoc/>
        public DbSet<Score> Scores { get; set; }
        /// <inheritdoc/>
        public DbSet<SwitchConfigFileEntry> Switches { get; set; }
        /// <inheritdoc/>
        public DbSet<StepperConfigFileEntry> Steppers { get; set; }
        /// <inheritdoc/>
        public DbSet<ServoConfigFileEntry> Servos { get; set; }
        /// <inheritdoc/>
        public DbSet<WS281xConfigFileEntry> WS281xLeds { get; set; }
        /// <inheritdoc/>
        public DbSet<Lpd8806ConfigFileEntry> Lpd8806Leds { get; set; }
        /// <inheritdoc/>
        public ILookup<string, Adjustment> AdjustmentLookUp { get; private set; }
        /// <inheritdoc/>
        public ILookup<string, Audit> AuditLookUp { get; private set; }
        /// <inheritdoc/>
        public Adjustment GetAdjustment(string key) => AdjustmentLookUp[key]?.FirstOrDefault();
        /// <inheritdoc/>
        public int GetAdjustmentValue(string key) => AdjustmentLookUp[key]?.FirstOrDefault()?.Value ?? -255;
        /// <inheritdoc/>
        public Audit GetAudit(string key) => AuditLookUp[key]?.FirstOrDefault();
        /// <inheritdoc/>
        public int GetAuditValue(string key) => AuditLookUp[key]?.FirstOrDefault()?.Value ?? -255;
        /// <inheritdoc/>
        public void IncrementAuditValue(string key, int increment)
        {
            var val = GetAuditValue(key);
            val += increment;
            SetAuditValue(key, val);
        }

        /// <inheritdoc/>
        public void SetAdjustmentValue(string key, int value) => AdjustmentLookUp[key].FirstOrDefault().Value = value;

        /// <inheritdoc/>
        public void SetAuditValue(string key, int value) => AuditLookUp[key].FirstOrDefault().Value = value;

        /// <inheritdoc/>
        public IEnumerable<Score> ScoresTop(int amt = 10)
        {
            return Scores
                .Include(p => p.Player)
                .Include(g => g.GamePlayed)
                .OrderByDescending(s => s.Points)
                .Take(amt).ToList();
        }
        /// <inheritdoc/>
        public IEnumerable<BallPlayed> ScoresBallsPlayed(int amt = 10)
        {
            return BallsPlayed
                .Include(p => p.Player)
                .Include(c => c.Score).ThenInclude(gp => gp.GamePlayed)
                .OrderByDescending(s => s.Points)
                .Take(amt).ToList();
        }

        /// <inheritdoc/>
        public MachineConfiguration GetMachineConfiguration()
        {
            var machine = Machine.Find(1);

            var coils = Coils.AsNoTracking().ToList();

            var mc = new MachineConfiguration()
            {
                //all switches
                PRSwitches = Switches.AsNoTracking()
                 .Select(x => x).ToList(),

                //all coils
                PRCoils = coils
                 .Select(x => x).ToList(),

                //all lamps
                PRLamps = Lamps.AsNoTracking()
                .Select(x => x).ToList(),

                //all leds
                PRLeds = Leds.AsNoTracking()
                .Select(x => x)
                .ToList(),

                //pd led steppers
                PRSteppers = Steppers.AsNoTracking()
                .Select(x => x)
                .ToList(),

                //pd led servos
                PRServos = Servos.AsNoTracking()
                .Select(x => x)
                .ToList(),

                //pd serial leds
                PRWs281x = WS281xLeds.AsNoTracking()
                .Select(x => x)
                .ToList(),
                PRLpd8806 = Lpd8806Leds.AsNoTracking()
                .Select(x => x)
                .ToList(),

                //all flipper switches.
                PRFlippers = Switches.AsNoTracking()
                .Where(x => x.ItemType == "flipper")
                .Select(x => x.Name)
                .ToList(),

                PRGame = new GameConfigFileEntry()
                {
                    DisplayMonitor = machine.DisplayMonitor,
                    MachineType = machine.MachineType,
                    NumBalls = machine.NumBalls
                },

                //all bumpers / slings for auto fire without code
                PRBumpers = Switches.AsNoTracking()
                    .Where(x => x.ItemType == "bumper")
                    .Select(x => x.Name)
                    .ToList(),

                //PRBallSearch - Created below
                //PRDriverAliases
                //PRGI
            };

            //ball search Reset switches
            Dictionary<string, string> resets = new Dictionary<string, string>();
            foreach (var item in mc.PRSwitches.Where(x => !string.IsNullOrWhiteSpace(x.SearchReset))
                .Select(x => new { x.Name, x.SearchReset }))
            {
                resets.Add(item.Name, item.SearchReset);
            }

            //ball search Stop switches
            Dictionary<string, string> stops = new Dictionary<string, string>();
            foreach (var item in mc.PRSwitches.Where(x => !string.IsNullOrWhiteSpace(x.SearchStop))
                .Select(x => new { x.Name, x.SearchStop }))
            {
                stops.Add(item.Name, item.SearchStop);
            }

            //init ball search config
            mc.PRBallSearch = new BallSearchConfigFileEntry()
            {
                PulseCoils = mc.PRCoils
                    .Where(x => x.Search > 0)?.Select(x => x.Name).ToList(),
                StopSwitches = stops,
                ResetSwitches = resets,
            };

            return mc;
        }

        /// <inheritdoc/>
        public void InitializeDatabase(bool isMachinePdb, bool delete = false)
        {
            try
            {
                if (delete) Database.EnsureDeleted();

                //migrations and create the database if doesn't exist
                Database.Migrate();

                //return if data is already populated
                if (Switches?.Count() > 0)
                {
                    CreateLookUpTables();
                    return;
                }

                string initFile = string.Empty;

                if (isMachinePdb)
                    initFile = Path.Combine(Directory.GetCurrentDirectory(), "sql/init_p3roc.sql");
                else
                    initFile = Path.Combine(Directory.GetCurrentDirectory(), "sql/init_proc.sql");

                if (File.Exists(initFile))
                {
                    var sql = File.ReadAllText(initFile);
                    Database.ExecuteSqlRaw(sql);                    
                }
                else
                    throw new FileNotFoundException($"{initFile} sql file not found");

                CreateLookUpTables();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// If context isn't configured then a local directory database using the <see cref="CONNECTION_STRING"/> will be used
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                System.Console.WriteLine($"{nameof(NetProcDbContext)} isn't configured, creating sqlite configuration for local netproc.db");
                optionsBuilder.UseSqlite(CONNECTION_STRING);
            }
        }

        /// <summary>
        /// Applies database table configurations
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CoilConfiguration).Assembly);

            //add default colours for wiring
            modelBuilder.Entity<ColorSet>().HasData(new ColorSet[]
            {
                new ColorSet(){ Name = "BLK", HtmlCode = "010101"},
                new ColorSet(){ Name = "WHT", HtmlCode = "FEFEFE"},
                new ColorSet(){ Name = "BLU", HtmlCode = "0155FE"},
                new ColorSet(){ Name = "RED", HtmlCode = "FF0000"},
                new ColorSet(){ Name = "YEL", HtmlCode = "EDFE01"},
                new ColorSet(){ Name = "ORG", HtmlCode = "FE9001"},
                new ColorSet(){ Name = "GRN", HtmlCode = "3CDF13"},
                new ColorSet(){ Name = "VIO", HtmlCode = "A913DF"}
            });
        }

        /// <summary>
        /// create a look up table for adjustments and audits
        /// </summary>
        private void CreateLookUpTables()
        {
            AdjustmentLookUp = Adjustments.ToLookup(x => x.Id);
            AuditLookUp = Audits.ToLookup(x => x.Id);
        }
    }
}
