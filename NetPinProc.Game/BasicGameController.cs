using NetPinProc.Domain;
using NetPinProc.Domain.Mode;
using NetPinProc.Domain.PinProc;
using System;

namespace NetPinProc.Game
{
    /// <summary>
    /// This class uses the <see cref="GameController"/> base with added helper methods and setup automation. <para/>
    /// - Tag your trough switches, early switches for a ball trough setup //TODO: setup ball search and saves
    /// </summary>
    public abstract class BasicGameController : GameController
    {
        /// <summary>
        /// Creates a trough mode. <see cref="GameController"/>
        /// </summary>
        /// <param name="machineType"></param>
        /// <param name="logger"></param>
        /// <param name="simulated"></param>
        /// <param name="configuration"></param>
        public BasicGameController(MachineType machineType, ILogger logger = null, bool simulated = false, MachineConfiguration configuration = null) : base(machineType, logger, simulated, configuration)
        {
            //BallSearch bs = new BallSearch()
            //BallSave bs = new BallSave(this, "");
        }

        /// <inheritdoc/>
        public override void BallEnded()
        {
            base.BallEnded();
            Logger.Log(nameof(BasicGameController) + ":" + nameof(BallEnded), LogLevel.Debug);
        }

        /// <returns>Trough BallSaveActive</returns>
        public virtual bool BallSaveActive() => Trough?.BallSaveActive() ?? false;

        /// <inheritdoc/>
        public override void BallStarting()
        {
            base.BallStarting();
            Logger.Log(nameof(BasicGameController) + ":" + nameof(BallStarting), LogLevel.Debug);
        }

        /// <inheritdoc/>
        public override void GameEnded()
        {
            base.GameEnded();
            Logger.Log(nameof(BasicGameController) + ":" + nameof(GameEnded), LogLevel.Debug);
        }

        /// <inheritdoc/>
        public override void GameStarted()
        {
            base.GameStarted();
            Logger.Log(nameof(BasicGameController) + ":" + nameof(GameStarted), LogLevel.Debug);
        }

        /// <summary>
        /// Resets game, adds trough mode, creates trough if doesn't exist.
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            Logger?.Log(nameof(BasicGameController) + ":" + nameof(Reset)+": adding trough to game modes.", LogLevel.Debug);
        }

        /// <inheritdoc/>
        public override void SetUp()
        {
            base.SetUp();
            Logger?.Log(nameof(BasicGameController) + ":" + nameof(SetUp) + ": game setup.", LogLevel.Debug);
        }

        /// <inheritdoc/>
        public override void ShootAgain()
        {
            base.ShootAgain();
            Logger.Log(nameof(BasicGameController) + ":" + nameof(ShootAgain), LogLevel.Debug);
        }

        /// <inheritdoc/>
        public override void StartBall()
        {
            base.StartBall();
            Logger.Log(nameof(BasicGameController) + ":" + nameof(StartBall), LogLevel.Debug);
        }

        /// <inheritdoc/>
        public override void StartGame()
        {
            base.StartGame();
            Logger.Log(nameof(BasicGameController) + ":" + nameof(StartGame), LogLevel.Debug);
        }

        /// <inheritdoc/>
        public override void UpdateLamps()
        {
            base.UpdateLamps();            
            if(Modes?.Modes?.Count > 0)
            {
                Logger.Log(nameof(BasicGameController) + ":" + nameof(UpdateLamps) + ": updating all modes lamps", LogLevel.Debug);
                Modes.Modes.ForEach(x => x.UpdateLamps());
            }
            else { Logger.Log(nameof(BasicGameController) + ":" + nameof(UpdateLamps) + ": no modes running", LogLevel.Warning); }
        }
    }
}
