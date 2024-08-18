using NetPinProc.Domain.Pdb;

namespace NetPinProc.Game.Tests.GameTests.P3_ROC
{
    /// <summary>Test RGBs on PD LED boards for P3ROC-PDB</summary>
    public class PdLed_RGB_Tests : GameContollerTestBase
    {
        /// <summary>Change base parameter to true to simulate</summary>
        public PdLed_RGB_Tests() : base(false) { }

        /// <summary>Adds a script to every led found in the machine.json<para/>
        /// The games loop is run then stopped after 10 seconds of fading</summary>
        /// <returns></returns>
        [Fact]
        public async Task ScriptRedGreenBlue_ToAllLEDS_For10secs_Tests()
        {
            Assert.True(_game?.LEDS?.Count > 0);

            var script = new LEDScript[1]
            {
                    new LEDScript(){ Colour = new uint[] { 0xFF, 0, 0}, Duration = 500, FadeTime = 500},
                    //new LEDScript(){ Colour = new uint[] { 0, 0xFF, 0}, Duration = 1000, FadeTime = 2000},
                    //new LEDScript(){ Colour = new uint[] { 0, 0, 0xFF}, Duration = 1000, FadeTime = 2000}
            };

            
            foreach (var led in _game.LEDS)
            {
                //led.Value.Script(script);

                //GBR - green, blue, red
                // - Polarity true
                // - These are solid colors but more transparent when polarity is set to false. 
                led.Value.Color(new uint[] { 0xFF, 0, 0 }); //GREEN
                led.Value.Color(new uint[] { 0, 0xFF, 0 }); //BLUE
                led.Value.Color(new uint[] { 0, 0, 0xFF }); //RED
                led.Value.Color(new uint[] { 0xFF, 0, 0xFF }); //YELLOW                
            }

            _ = base.RunGameLoop(10);

            await Task.Delay(10000);

            foreach (var led in _game.LEDS) { led.Value.Disable(); }

            _game.EndRunLoop();
        }
    }
}
