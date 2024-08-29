using Microsoft.EntityFrameworkCore;
using NetPinProc.Game.Sqlite.Model;
using NetPinProc.Domain;
using NetPinProc.Domain.MachineConfig;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NetPinProc.Domain.Data;

namespace NetPinProc.Game.Sqlite
{
    /// <summary>
    /// Entity framework repo interface
    /// </summary>
    public interface INetProcDbContext
    {
        /// <summary> Lookup for adjustments by KEY. eg: DISP_X </summary>
        ILookup<string, Adjustment> AdjustmentLookUp { get; }

        /// <summary> machine / game adjustments</summary>
        DbSet<Adjustment> Adjustments { get; set; }

        /// <summary>Lookup for audits by KEY. eg: CREDITS</summary>
        ILookup<string, Audit> AuditLookUp { get; }

        /// <summary>Audits from machine</summary>
        DbSet<Audit> Audits { get; set; }

        /// <summary>Ball played, score of ball, time</summary>
        DbSet<BallPlayed> BallsPlayed { get; set; }

        /// <summary> Stored database coils</summary>
        DbSet<CoilConfigFileEntry> Coils { get; set; }

        /// <summary>Colour sets for wiring, UI. Sets have a short name and color</summary>
        DbSet<ColorSet> Colors { get; set; }

        /// <summary>Games played log with scores and players</summary>
        DbSet<GamePlayed> GamesPlayed { get; set; }

        /// <summary>Stored database lamps</summary>
        DbSet<LampConfigFileEntry> Lamps { get; set; }

        /// <summary>Stored database leds</summary>
        DbSet<LedConfigFileEntry> Leds { get; set; }

        /// <summary>Stored database leds</summary>
        DbSet<Lpd8806ConfigFileEntry> Lpd8806Leds { get; set; }

        /// <summary>Machine config</summary>
        DbSet<Machine> Machine { get; set; }

        /// <summary>Parts on the machine, admin use</summary>
        DbSet<Part> Parts { get; set; }

        /// <summary>Players on the machine</summary>
        DbSet<Player> Players { get; set; }

        /// <summary>Stored database scores</summary>
        DbSet<Score> Scores { get; set; }

        /// <summary>Stored database servos for pdleds</summary>
        DbSet<ServoConfigFileEntry> Servos { get; set; }

        /// <summary>Stored database stepper motors for pdleds</summary>
        DbSet<StepperConfigFileEntry> Steppers { get; set; }

        /// <summary> Stored database switches</summary>
        DbSet<SwitchConfigFileEntry> Switches { get; set; }

        /// <summary> Serial leds</summary>
        DbSet<WS281xConfigFileEntry> WS281xLeds { get; set; }

        /// <summary>Dispose, close</summary>
        void Dispose();

        /// <summary>
        /// Returns -255 if failed to lookup
        /// </summary>
        /// <param name="key"></param>
        /// <returns>-255 if fail. Some adjustments will be using minus values, but not this low</returns>
        int GetAdjustmentValue(string key);
        /// <summary>
        /// Returns -255 if failed to lookup
        /// </summary>
        /// <param name="key"></param>
        /// <returns>-255 if fail. Some adjustments will be using minus values, but not this low</returns>
        int GetAuditValue(string key);

        /// <summary>Creates a <see cref="MachineConfiguration"/> from database entries <para/>
        /// All machine items, the game type and ball search switches <para/>
        /// Normally in a machine.json you would add "PRFlippers" to map the flippers and switch rules to coils but this finds the switches with an item type of flipper<para/>
        /// Similarly with PRBumpers, bumper switches set as item type bumper will be added to this list, you should do the same for slingshots = bumpers <para/>
        /// PRBallSearch in a machine.json contains coils to pulse, switches to trigger stop and reset on a ball search...<para/>
        ///    Any driver found in the config with a search pulse greater than 0 will be fired in a ball search
        ///    Set open or closed on the switches ball search properties
        /// </summary>
        /// <returns></returns>
        MachineConfiguration GetMachineConfiguration();

        /// <summary>
        /// Increments an audit value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="increment"></param>
        void IncrementAuditValue(string key, int increment);
        /// <summary>
        /// Runs migrate which creates the database, then runs the init_proc.sql to seed data depending on the machine type.<para/>
        /// If the database already created it will skip initializing, just for safety...
        /// </summary>
        /// <param name="isMachinePdb">If true uses the init_p3roc.sql</param>
        /// <param name="delete">Delete the database before initializing?</param>
        void InitializeDatabase(bool isMachinePdb, bool delete = false);
        /// <summary>
        /// Saves all changes, do at the end of game
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
        /// <summary>
        /// Saves all changes, do at the end of game
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken=default);
        /// <summary>
        /// Gets top scores per ball. Best balls.
        /// </summary>
        /// <param name="amt"></param>
        /// <returns></returns>
        IEnumerable<BallPlayed> ScoresBallsPlayed(int amt = 10);
        /// <summary>
        /// Gets top scores
        /// </summary>
        /// <param name="amt"></param>
        /// <returns></returns>
        IEnumerable<Score> ScoresTop(int amt = 10);

        /// <summary>
        /// Sets value in adjustment lookup
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void SetAdjustmentValue(string key, int value);
        /// <summary>
        /// Sets value in audit lookup
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void SetAuditValue(string key, int value);
    }
}
