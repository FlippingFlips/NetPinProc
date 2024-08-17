using NetPinProc.Domain;
using NetPinProc.Domain.MachineConfig;
using NetPinProc.Domain.Pdb;
using NetPinProc.Domain.PinProc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NetProc.ProcDevice
{
    class Program
    {
        static CancellationTokenSource source = new CancellationTokenSource();
        private static AttrCollection<ushort, string, Switch> _switches;
        private static AttrCollection<ushort, string, LED> _leds;
        private static AttrCollection<ushort, string, IDriver> _coils;
        private static AttrCollection<ushort, string, PdStepper> _steppers;

        static async Task Main(string[] args)
        {
            IProcDevice PROC = null;
            try
            {
                Console.WriteLine("Creating PROC");
                PROC = new NetPinProc.ProcDevice(MachineType.PDB, new ConsoleLogger());
                await Task.Delay(100);
                PROC?.Reset(1);

                //load machine config.
                var config = MachineConfiguration.FromFile("machine.json");

                for (int i = 0; i < 222; i++)
                {
                    config.AddSwitch("switch" + i, i.ToString(), SwitchType.NO);
                }

                //create collections to pass into the setup.
                //The GameController does this for you in LoadConfig, but this is to test without a game
                _switches = new AttrCollection<ushort, string, Switch>();
                _leds = new AttrCollection<ushort, string, LED>();
                _coils = new AttrCollection<ushort, string, IDriver>();
                _steppers = new AttrCollection<ushort, string, PdStepper>();

                //config.AddStepper("Stepper", 0, 140, true);

                //setup machine items to be able to process events.
                PROC.SetupProcMachine(config, _switches: _switches, _leds: _leds, _coils: _coils, _steppers: _steppers);

                var states = PROC.SwitchGetStates();

                //listen for cancel keypress and end run loop
                Console.CancelKeyPress += (sender, eventArgs) =>
                {
                    Console.WriteLine("ctrl+C triggered");
                    source.Cancel();
                    eventArgs.Cancel = true;
                };


                //run game loop
                await RunLoop(PROC);

                //close console
                Console.WriteLine("NetPinProcgame closing...");
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        static async Task RunLoop(IProcDevice proc)
        {
            long loops = 0;
            Event[] events;
            //_coils["trough"].Pulse(255);

            var flasher = _coils["flasher"];

            //flasher.Pulse(255);

            //flasher.Schedule(0x4F4F4F4F, 10); //schedule for 10 sec

            //flasher.Patter(0, 0, 0); // on 1, off 1
            //flasher.Patter(125, 0, 0); // on 10ms, off 1

            //flasher.Patter(10, 255); //flash fast
            //flasher.Patter(255, 255); //flash med
            //flasher.Patter(10, 10); //always on (pretty much)

            //flasher.Enable();

            var led = _leds["TestLED"];

            led.ChangeColor(new uint[] { 0xFF, 0, 0 });

            var stepper = _steppers["Stepper"];
            //stepper.Stop();
            stepper.Move(-5000);


            while (!source.IsCancellationRequested)
            {
                loops++;
                await Task.Delay(1000);
                events = proc.Getevents();
                if (events != null)
                {
                    foreach (Event evt in events)
                    {
                        if (evt.Type != EventType.None && evt.Type != EventType.Invalid)
                        {
                            Console.WriteLine($"{evt.Type} event");
                            Switch sw = _switches[(ushort)evt.Value];
                            bool recvd_state = evt.Type == EventType.SwitchClosedDebounced;
                            if (!sw.IsState(recvd_state))
                            {
                                Console.WriteLine($"{sw.Name} {recvd_state}");
                                sw.SetState(recvd_state);
                            }
                        }
                    }
                }

                proc.WatchDogTickle();

                stepper.Move(16000);
            }

            proc.Close();
            //return Task.CompletedTask;
        }
    }
}
