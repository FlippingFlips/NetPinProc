using NetPinProc.Game.Sqlite.Model;

namespace NetPinProc.Game.Sqlite.Tests.Database_Tests
{
    public class NetProcDbTests
    {
        /// <summary>Delete and create the database. Check we have some adjustments from the seed data and add a game played. <para/>
        /// THIS DOESN'T CREATE A GAME CONTROLLER
        /// </summary>
        [Fact]
        public void InitializeDatabase_Tests()
        {
            //create context
            using var ctx = new NetProcDbContext();

            //init database using a p3-roc PDB
            ctx.InitializeDatabase(true, true);

            //assert we have at least 1 adjustment after 
            var adjustments = ctx.Adjustments?.Count() ?? 0;
            Assert.True(adjustments > 0);

            ctx.Add(new GamePlayed
            {
                Started = DateTime.Now.AddMinutes(-10),
                Ended = DateTime.Now,
                Scores = new List<Score> { new Score
                 {
                    PlayerId = 1,
                    Points = 2000000
                 }}
            });
            ctx.SaveChanges();
        }
    }
}