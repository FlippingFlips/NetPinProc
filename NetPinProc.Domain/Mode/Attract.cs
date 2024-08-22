namespace NetPinProc.Domain.Mode
{
    /// <summary>Base attract mode that just handles starting a game</summary>
    public class Attract : Mode
    {
        /// <summary>
        /// Init
        /// </summary>
        /// <param name="game"></param>
        /// <param name="priority"></param>
        public Attract(IGameController game, int priority) : base(game, priority) { }

        /// <summary>Start button, starts game and adds a player if the trough is full</summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public virtual bool sw_start_active(Switch sw)
        {
            Game.Logger?.Log("start button active", PinProc.LogLevel.Debug);
            if (Game.Trough?.IsFull() ?? false) //todo: credit check?
            {
                Game.Logger.Log("start button, trough full", PinProc.LogLevel.Debug);
                Game.StartGame();
                Game.AddPlayer();
                Game.StartBall();
                Game.Modes.Remove(this);
            }
            else
            {
                Game.Logger?.Log("attract start. trough balls:" + Game.Trough.NumBalls(), PinProc.LogLevel.Debug);
                //TODO: Ball search
            }
            return SWITCH_CONTINUE;
        }
    }
}
