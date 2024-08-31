using NetPinProc.Domain;
using NetPinProc.Domain.MachineConfig;
using NetPinProc.Domain.Pdb;
using NetPinProc.Domain.PinProc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace NetPinProc
{
    /// <summary>
    /// Wrapper for NetPinProcGame (libpinproc native interface).
    /// </summary>
    public class ProcDevice : IProcDevice, IDisposable
    {
        /// <summary>
        /// Type of machine the PROC is working with
        /// </summary>
        public MachineType g_machineType;
        /// <summary>
        /// Handle to the PROC device. Initialized in constructor
        /// </summary>
        public IntPtr ProcHandle;
        /// <summary>
        /// 
        /// </summary>
        public bool swCoindoor = false;
        /// <summary>
        /// TODO: implement coil driver object list
        /// </summary>
        protected internal AttrCollection<ushort, string, IDriver> Coils;

        private static bool firstTime = true;
        static readonly ushort kDMDColumns = 128;
        static readonly byte kDMDFrameBuffers = 3;
        static readonly byte kDMDRows = 32;
        readonly static byte kDMDSubFrames = 4;
        private bool dmdConfigured = false;
        readonly byte[] dmdMapping;
        private readonly int dmdMappingSize = 16;
        private readonly object procSyncObject = new object();
        readonly byte[] testFrame = new byte[128 * 32];

        /// <summary>
        /// Gets the <see cref="ProcHandle"/> when creating, see <see cref="PinProc.PRCreate(MachineType)"/>
        /// </summary>
        /// <param name="machineType"></param>
        /// <param name="logger"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public ProcDevice(MachineType machineType, ILoggerPROC logger = null)
        {
            this.Logger = logger;

            NativeLoadLibPinProcDll();

            Logger?.Log("Initializing P-ROC device...", LogLevel.Info);

            dmdMapping = new byte[dmdMappingSize];
            for (int i = 0; i < dmdMappingSize; i++)
                dmdMapping[i] = (byte)i;

            g_machineType = machineType;

            dmdConfigured = false;

            ProcHandle = PinProc.PRCreate(machineType);
            if (ProcHandle == IntPtr.Zero)
            {
                //throw new InvalidOperationException(PinProc.PRGetLastErrorText());
                throw new InvalidOperationException("No P-ROC or P3-ROC found\n\n");
            }                

            this.Coils = new AttrCollection<ushort, string, IDriver>();
        }

        /// <summary>
        /// 
        /// </summary>
        public ILoggerPROC Logger { get; set; }

        /// <summary>
        /// TODO: Send aux commands Not implemented yet
        /// </summary>
        /// <param name="address"></param>
        /// <param name="aux_commands"></param>
        public void AuxSendCommands(ushort address, ushort aux_commands)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the handle to PinProc. <see cref="PinProc.PRDelete(IntPtr)"/>
        /// </summary>
        public void Close()
        {
            if (ProcHandle != IntPtr.Zero)
                PinProc.PRDelete(ProcHandle);

            ProcHandle = IntPtr.Zero;

            Dispose();
        }

        /// <summary>
        /// See <see cref="PinProc.PRDMDUpdateConfig(IntPtr, ref DMDConfig)"/> and <see cref="PinProc.PRDMDDraw(IntPtr, byte[])"/>
        /// </summary>
        /// <param name="bytes"></param>
        public void DmdDraw(byte[] bytes)
        {
            if (!dmdConfigured)
            {
                DMDConfig dmdConfig = new (kDMDColumns, kDMDRows);
                DMDConfigPopulateDefaults(ref dmdConfig);
                PinProc.PRDMDUpdateConfig(ProcHandle, ref dmdConfig);
                dmdConfigured = true;
            }
            PinProc.PRDMDDraw(ProcHandle, bytes);
        }

        /// <summary>
        /// Uses <see cref="DmdDraw(byte[])"/> and configures PinProc DMD if not configured
        /// </summary>
        /// <param name="frame"></param>
        public void DmdDraw(IFrame frame)
        {
            if (!dmdConfigured)
            {
                DMDConfig dmdConfig = new (kDMDColumns, kDMDRows);
                DMDConfigPopulateDefaults(ref dmdConfig);
                PinProc.PRDMDUpdateConfig(ProcHandle, ref dmdConfig);
                dmdConfigured = true;
            }
            //dmd_draw(testFrame);
            byte[] dots = new byte[4 * kDMDColumns * kDMDRows / 8];
            DMDGlobals.DMDFrameCopyPROCSubframes(frame.Frame, dots, kDMDColumns, kDMDRows, 4, dmdMapping);
            DmdDraw(dots);
        }

        /// <summary>
        /// Updates the DMD config, see <see cref="PinProc.PRDMDUpdateConfig(IntPtr, ref DMDConfig)"/>
        /// </summary>
        /// <param name="high_cycles"></param>
        public void DmdUpdateConfig(ushort[] high_cycles)
        {
            DMDConfig dmdConfig = new ();
            DMDConfigPopulateDefaults(ref dmdConfig);
            if (high_cycles == null || high_cycles.Length != 4)
                return;

            for (int i = 0; i < 4; i++)
            {
                dmdConfig.DeHighCycles[i] = high_cycles[i];
            }
            lock (procSyncObject)
            {
                PinProc.PRDMDUpdateConfig(ProcHandle, ref dmdConfig);
            }
            dmdConfigured = true;

        }

        /// <summary>
        /// See <see cref="PinProc.PRDriverUpdateGroupConfig(IntPtr, ref DriverGroupConfig)"/>
        /// </summary>
        /// <param name="group_num"></param>
        /// <param name="slow_time"></param>
        /// <param name="enable_index"></param>
        /// <param name="row_activate_index"></param>
        /// <param name="row_enable_select"></param>
        /// <param name="matrixed"></param>
        /// <param name="polarity"></param>
        /// <param name="active"></param>
        /// <param name="disable_strobe_after"></param>
        public void DriverUpdateGroupConfig(byte group_num, ushort slow_time, byte enable_index, byte row_activate_index,
            byte row_enable_select, bool matrixed, bool polarity, bool active, bool disable_strobe_after)
        {
            lock (procSyncObject)
            {
                DriverGroupConfig group = new()
                {
                    GroupNum = group_num,
                    SlowTime = slow_time,
                    EnableIndex = enable_index,
                    RowActivateIndex = row_activate_index,
                    RowEnableSelect = row_enable_select,
                    Matrixed = matrixed,
                    Polarity = polarity,
                    Active = active,
                    DisableStrobeAfter = disable_strobe_after
                };

                PinProc.PRDriverUpdateGroupConfig(ProcHandle, ref group);
            }
        }

        /// <summary>
        /// See <see cref="PinProc.PRDriverStateDisable(ref DriverState)"/>
        /// </summary>
        /// <param name="number"></param>
        public void DriverDisable(ushort number)
        {
            DriverState state = this.DriverGetState(number);
            lock (procSyncObject)
            {
                PinProc.PRDriverStateDisable(ref state);
                PinProc.PRDriverUpdateState(ProcHandle, ref state);
                PinProc.PRFlushWriteData(ProcHandle);
            }
        }

        /// <summary>
        /// See <see cref="PinProc.PRDriverStateFuturePulse(ref DriverState, ushort, ushort)"/>
        /// </summary>
        /// <param name="number"></param>
        /// <param name="milliseconds"></param>
        /// <param name="futureTime"></param>
        /// <returns></returns>
        public Result DriverFuturePulse(ushort number, byte milliseconds, UInt16 futureTime)
        {
            DriverState state = this.DriverGetState(number);
            Result res;

            lock (procSyncObject)
            {
                PinProc.PRDriverStateFuturePulse(ref state, milliseconds, futureTime);
                res = PinProc.PRDriverUpdateState(ProcHandle, ref state);
            }

            if (res == Result.Success)
            {
                lock (procSyncObject)
                {
                    res = PinProc.PRDriverWatchdogTickle(ProcHandle);
                    res = PinProc.PRFlushWriteData(ProcHandle);
                }
            }
            return res;
        }

        /// <summary>
        /// See <see cref="PinProc.PRDriverGetState(IntPtr, byte, ref DriverState)"/>
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public DriverState DriverGetState(ushort number)
        {
            DriverState ds = new ();
            lock (procSyncObject)
            {
                PinProc.PRDriverGetState(ProcHandle, (byte)number, ref ds);
            }
            return ds;
        }

        /// <summary>
        /// See <see cref="PinProc.PRDriverGroupDisable(IntPtr, byte)"/>
        /// </summary>
        /// <param name="number"></param>
        public void DriverGroupDisable(byte number)
        {
            lock (procSyncObject)
            {
                PinProc.PRDriverGroupDisable(ProcHandle, number);
            }
        }

        /// <summary>
        /// See <see cref="PinProc.PRDriverStatePatter(ref DriverState, ushort, ushort, ushort)"/>
        /// </summary>
        /// <param name="number"></param>
        /// <param name="milliseconds_on"></param>
        /// <param name="milliseconds_off"></param>
        /// <param name="original_on_time"></param>
        public void DriverPatter(ushort number, ushort milliseconds_on, ushort milliseconds_off, ushort original_on_time)
        {
            DriverState state = this.DriverGetState(number);
            lock (procSyncObject)
            {
                PinProc.PRDriverStatePatter(ref state, milliseconds_on, milliseconds_off, original_on_time);
                PinProc.PRDriverUpdateState(ProcHandle, ref state);
                PinProc.PRFlushWriteData(ProcHandle);
            }
        }

        /// <summary>
        /// See <see cref="PinProc.PRDriverStatePulse(ref DriverState, byte)"/>
        /// </summary>
        /// <param name="number"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public Result DriverPulse(ushort number, byte milliseconds)
        {
            DriverState state = this.DriverGetState(number);
            Result res;
            lock (procSyncObject)
            {
                PinProc.PRDriverStatePulse(ref state, milliseconds);
                res = PinProc.PRDriverUpdateState(ProcHandle, ref state);
            }

            if (res == Result.Success)
            {
                lock (procSyncObject)
                {
                    res = PinProc.PRDriverWatchdogTickle(ProcHandle);
                    res = PinProc.PRFlushWriteData(ProcHandle);
                }
            }
            return res;
        }

        /// <summary>
        /// See <see cref="PinProc.PRDriverStatePulsedPatter(ref DriverState, ushort, ushort, ushort)"/>
        /// </summary>
        /// <param name="number"></param>
        /// <param name="milliseconds_on"></param>
        /// <param name="milliseconds_off"></param>
        /// <param name="milliseconds_overall_patter_time"></param>
        public void DriverPulsedPatter(ushort number, ushort milliseconds_on, ushort milliseconds_off, ushort milliseconds_overall_patter_time)
        {
            DriverState state = this.DriverGetState(number);
            lock (procSyncObject)
            {
                PinProc.PRDriverStatePulsedPatter(ref state, milliseconds_on, milliseconds_off, milliseconds_overall_patter_time);
                PinProc.PRDriverUpdateState(ProcHandle, ref state);
                PinProc.PRFlushWriteData(ProcHandle);
            }
        }

        /// <summary>
        /// Assigns repeating schedule, see <see cref="PinProc.PRDriverStateSchedule(ref DriverState, uint, byte, bool)"/>
        /// </summary>
        /// <param name="number"></param>
        /// <param name="schedule"></param>
        /// <param name="cycle_seconds"></param>
        /// <param name="now"></param>
        public void DriverSchedule(ushort number, uint schedule, ushort cycle_seconds, bool now)
        {
            DriverState state = this.DriverGetState(number);
            lock (procSyncObject)
            {
                PinProc.PRDriverStateSchedule(ref state, schedule, (byte)cycle_seconds, now);
                PinProc.PRDriverUpdateState(ProcHandle, ref state);
                PinProc.PRFlushWriteData(ProcHandle);
            }
        }

        /// <summary>
        /// Disables (turns off) the given driver.
        /// This function is provided for convenience. See <see cref="PinProc.PRDriverStateDisable(ref DriverState)"/> for a full description.</summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public DriverState DriverStateDisable(DriverState state)
        {
            lock (procSyncObject)
            {
                PinProc.PRDriverStateDisable(ref state);
            }
            return state;
        }

        /// <summary>
        /// See <see cref="PinProc.PRDriverStateFuturePulse(ref DriverState, ushort, ushort)"/>
        /// </summary>
        /// <param name="state"></param>
        /// <param name="milliseconds"></param>
        /// <param name="futureTime"></param>
        /// <returns></returns>
        public DriverState DriverStateFuturePulse(DriverState state, byte milliseconds, UInt16 futureTime)
        {
            lock (procSyncObject)
            {
                PinProc.PRDriverStateFuturePulse(ref state, milliseconds, futureTime);
            }
            return state;
        }

        /// <summary>
        /// See <see cref="PinProc.PRDriverStatePatter(ref DriverState, ushort, ushort, ushort)"/>
        /// </summary>
        /// <param name="state"></param>
        /// <param name="milliseconds_on"></param>
        /// <param name="milliseconds_off"></param>
        /// <param name="original_on_time"></param>
        /// <returns></returns>
        public DriverState DriverStatePatter(DriverState state, ushort milliseconds_on, ushort milliseconds_off, ushort original_on_time)
        {
            lock (procSyncObject)
            {
                PinProc.PRDriverStatePatter(ref state, milliseconds_on, milliseconds_off, original_on_time);
            }
            return state;
        }

        /// <summary>
        /// see <see cref="PinProc.PRDriverStatePulse(ref DriverState, byte)"/>
        /// </summary>
        /// <param name="state"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public DriverState DriverStatePulse(DriverState state, byte milliseconds)
        {
            lock (procSyncObject)
            {
                PinProc.PRDriverStatePulse(ref state, milliseconds);
            }
            return state;
        }

        /// <summary>
        /// See <see cref="PinProc.PRDriverStatePulsedPatter(ref DriverState, ushort, ushort, ushort)"/>
        /// </summary>
        /// <param name="state"></param>
        /// <param name="milliseconds_on"></param>
        /// <param name="milliseconds_off"></param>
        /// <param name="milliseconds_overall_patter_time"></param>
        /// <returns></returns>
        public DriverState DriverStatePulsedPatter(DriverState state, ushort milliseconds_on, ushort milliseconds_off, ushort milliseconds_overall_patter_time)
        {
            lock (procSyncObject)
            {
                PinProc.PRDriverStatePulsedPatter(ref state, milliseconds_on, milliseconds_off, milliseconds_overall_patter_time);
            }
            return state;
        }

        /// <summary>
        /// See <see cref="PinProc.PRDriverStateSchedule(ref DriverState, uint, byte, bool)"/>
        /// </summary>
        /// <param name="state"></param>
        /// <param name="schedule"></param>
        /// <param name="seconds"></param>
        /// <param name="now"></param>
        /// <returns></returns>
        public DriverState DriverStateSchedule(DriverState state, uint schedule, byte seconds, bool now)
        {
            lock (procSyncObject)
            {
                PinProc.PRDriverStateSchedule(ref state, schedule, seconds, now);
            }
            return state;
        }

        /// <summary>
        /// See <see cref="PinProc.PRDriverUpdateGlobalConfig(IntPtr, ref DriverGlobalConfig)"/>
        /// </summary>
        /// <param name="enable"></param>
        /// <param name="polarity"></param>
        /// <param name="use_clear"></param>
        /// <param name="strobe_start_select"></param>
        /// <param name="start_strobe_time"></param>
        /// <param name="matrix_row_enable_index0"></param>
        /// <param name="matrix_row_enable_index1"></param>
        /// <param name="active_low_matrix_rows"></param>
        /// <param name="tickle_stern_watchdog"></param>
        /// <param name="encode_enables"></param>
        /// <param name="watchdog_expired"></param>
        /// <param name="watchdog_enable"></param>
        /// <param name="watchdog_reset_time"></param>
        public void DriverUpdateGlobalConfig(bool enable, bool polarity, bool use_clear, bool strobe_start_select,
            byte start_strobe_time, byte matrix_row_enable_index0, byte matrix_row_enable_index1,
            bool active_low_matrix_rows, bool tickle_stern_watchdog, bool encode_enables, bool watchdog_expired,
            bool watchdog_enable, ushort watchdog_reset_time)
        {
            lock (procSyncObject)
            {
                DriverGlobalConfig globals = new()
                {
                    EnableOutputs = enable,
                    GlobalPolarity = polarity,
                    UseClear = use_clear,
                    StrobeStartSelect = strobe_start_select,
                    StartStrobeTime = start_strobe_time,
                    MatrixRowEnableIndex0 = matrix_row_enable_index0,
                    MatrixRowEnableIndex1 = matrix_row_enable_index1,
                    ActiveLowMatrixRows = active_low_matrix_rows,
                    TickleSternWatchdog = tickle_stern_watchdog,
                    EncodeEnables = encode_enables,
                    WatchdogExpired = watchdog_expired,
                    WatchdogEnable = watchdog_enable,
                    WatchdogResetTime = watchdog_reset_time
                };

                PinProc.PRDriverUpdateGlobalConfig(ProcHandle, ref globals);
            }
        }

        /// <summary>
        /// See <see cref="PinProc.PRDriverUpdateState(IntPtr, ref DriverState)"/>
        /// </summary>
        /// <param name="driver"></param>
        public void DriverUpdateState(ref DriverState driver)
        {
            lock (procSyncObject)
            {
                PinProc.PRDriverUpdateState(ProcHandle, ref driver);
            }
        }

        /// <summary>
        /// Flush all pending data from <see cref="ProcHandle"/> see <see cref="PinProc.PRFlushWriteData(IntPtr)"/>
        /// </summary>
        public void Flush()
        {
            lock (procSyncObject)
            {
                PinProc.PRFlushWriteData(ProcHandle);
            }
        }

        /// <summary>
        /// Gets events from PinProc. (see <see cref="PinProc.PRGetEvents"/>)
        /// </summary>
        /// <param name="dmdEvents">get DMD events?</param>
        /// <returns>null if no events</returns>
        public Event[] Getevents(bool dmdEvents = true)
        {
            const int batchSize = 16; // Pyprocgame uses 16
            Event[] events = new Event[batchSize];

            int numEvents;

            lock (procSyncObject)
            {
                numEvents = PinProc.PRGetEvents(ProcHandle, events, batchSize);
            }

            if (numEvents <= 0) return null;

            return events;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="register"></param>
        /// <param name="value"></param>
		public void i2c_write8(uint address, uint register, uint value)
        {
            this.WriteData(7, address << 9 | register, ref value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
		public void initialize_i2c(uint address)
        {
            this.i2c_write8(address, 0x00, 0x11); // Set sleep
            this.i2c_write8(address, 0x01, 0x04); // Configure output
                                                  //this.i2c_write8 (address, 0xFE, 130); // Set to 50Hz
            this.i2c_write8(address, 0xFE, 102); // Set to 60Hz
            Thread.Sleep(10); // Sleep needed to sync PLL
            this.i2c_write8(address, 0x00, 0x01); // No more sleeping
            Thread.Sleep(10);
        }

        /// <summary>
        /// See <see cref="PinProc.PRReadData(IntPtr, uint, uint, int, ref uint)"/>
        /// </summary>
        /// <param name="module"></param>
        /// <param name="startingAddr"></param>
        /// <param name="data"></param>
        /// <returns></returns>
		public Result ReadData(uint module, uint startingAddr, ref uint data)
        {
            Result r;

            lock (procSyncObject)
            {
                //Logger.Log(String.Format("read_data - thread: {0}", System.Threading.Thread.CurrentThread.Name));
                r = PinProc.PRReadData(ProcHandle, module, startingAddr, 1, ref data);
                //Logger.Log(String.Format ("read_data module: {0} start_addr: {1} data: {2}",
                //                          module, startingAddr, data));
            }
            return r;
        }

        /// <summary>
        /// See <see cref="PinProc.PRReset(IntPtr, uint)"/>
        /// </summary>
        /// <param name="flags"></param>
        public void Reset(uint flags)
        {
            lock (procSyncObject)
            {
                PinProc.PRReset(ProcHandle, flags);
            }
        }

        /// <summary>
        /// Does nothing
        /// </summary>
        /// <param name="mapping"></param>
        public void SetDmdColorMapping(byte[] mapping) { }

        /// <summary>
        /// Sets up all PROC driver and switch rules from a machine config. Pass in attribute collections which are populated here. <para/>
        /// </summary>
        /// <param name="config"></param>
        /// <param name="_coils"></param>
        /// <param name="_switches"></param>
        /// <param name="_lamps"></param>
        /// <param name="_leds"></param>
        /// <param name="_gi"></param>
        /// <param name="_steppers"></param>
        /// <param name="_servos"></param>
        /// <param name="_serialLeds"></param>
        public void SetupProcMachine(MachineConfiguration config,
            AttrCollection<ushort, string, IDriver> _coils = null,
            AttrCollection<ushort, string, Switch> _switches = null,
            AttrCollection<ushort, string, IDriver> _lamps = null,
            AttrCollection<ushort, string, LED> _leds = null,
            AttrCollection<ushort, string, IDriver> _gi = null,
            AttrCollection<ushort, string, PdStepper> _steppers = null,
            AttrCollection<ushort, string, PdServo> _servos = null,
            AttrCollection<ushort, string, PdSerialLed> _serialLeds = null)
        {
            List<VirtualDriver> new_virtual_drivers = new();
            bool polarity = (g_machineType == MachineType.SternWhitestar || g_machineType == MachineType.SternSAM || g_machineType == MachineType.PDB);

            PDBConfig pdb_config = null;
            if (g_machineType == MachineType.PDB)
                pdb_config = new PDBConfig(this, config);

            //process and add coils, add virtual driver, drivers
            if (_coils != null)
            {
                foreach (CoilConfigFileEntry ce in config.PRCoils)
                {
                    Driver d;
                    int number;
                    if (g_machineType == MachineType.PDB && pdb_config != null)
                    {
                        number = pdb_config.GetProcNumber("PRCoils", ce.Number);

                        if (number == -1)
                        {
                            Logger.Log($"Coil {ce.Name} cannot be controlled by the P-ROC. Ignoring...", LogLevel.Warning);
                            continue;
                        }
                    }
                    else
                        number = PinProc.PRDecode(g_machineType, ce.Number);

                    if ((ce.Bus != null && ce.Bus == "AuxPort") || number >= PinProcConstants.kPRDriverCount)
                    {
                        d = new VirtualDriver(this, ce.Name, (ushort)number, polarity);
                        new_virtual_drivers.Add((VirtualDriver)d);
                    }
                    else
                    {
                        d = new Driver(this, ce.Name, (ushort)number);
                        Logger?.Log("Adding driver " + d.ToString(), LogLevel.Verbose);
                        d.ReConfigure(ce.Polarity);
                    }

                    //set pulse time from the config
                    d.PulseTime = ce.PulseTime <= 0 ? (byte)20 : (byte)ce.PulseTime;
                    _coils.Add(d.Number, d.Name, d);
                }
            }

            if (_switches != null)
            {
                foreach (SwitchConfigFileEntry se in config.PRSwitches)
                {
                    //Log (se.Number);
                    var s = new Switch(this, se.Name, PinProc.PRDecode(g_machineType, se.Number), se.Type);

                    ushort number = 0;
                    if (g_machineType == MachineType.PDB)
                    {
                        var num = pdb_config.GetProcNumber("PRSwitches", se.Number);
                        if (num == -1)
                        {
                            Logger.Log($"Switch {se.Name} cannot be controlled by the P-ROC. Ignoring...", LogLevel.Warning);
                            continue;
                        }
                        else
                        {
                            number = Convert.ToUInt16(num);
                        }
                    }
                    else
                    {
                        number = PinProc.PRDecode(g_machineType, se.Number);
                    }

                    s.Number = number;
                    SwitchUpdateRule(number,
                        EventType.SwitchClosedDebounced,
                        new SwitchRule { NotifyHost = true, ReloadActive = false },
                        null,
                        false
                    );
                    SwitchUpdateRule(number,
                        EventType.SwitchOpenDebounced,
                        new SwitchRule { NotifyHost = true, ReloadActive = false },
                        null,
                        false
                    );
                    Logger?.Log("Adding switch " + s.ToString(), LogLevel.Verbose);
                    _switches.Add(s.Number, s.Name, s);
                }

                // TODO: THIS SHOULD RETURN A LIST OF STATES
                EventType[] states = SwitchGetStates();
                foreach (Switch s in _switches.Values)
                {
                    s.SetState(states[s.Number] == EventType.SwitchClosedDebounced);
                }
            }

            //process and add leds
            if (g_machineType == MachineType.PDB && _leds != null)
            {
                ushort i = 0;
                foreach (LedConfigFileEntry le in config.PRLeds)
                {
                    LED led = null;
                    led = new LED(this, le.Name, i, le.Number);
                    string number;
                    number = le.Number;
                    led.Polarity = le.Polarity;
                    _leds.Add(i, led.Name, led);
                    i++;
                }
            }

            //process and add steppers
            if(g_machineType == MachineType.PDB && _steppers != null)
            {
                ushort i = 0;
                foreach (StepperConfigFileEntry st in config.PRSteppers)
                {
                    if (st.IsEnabled)
                    {
                        Switch sw = null;

                        //pass in the stop switch if one is set
                        if (!string.IsNullOrWhiteSpace(st.StopSwitch) && _switches.ContainsKey(st.StopSwitch))
                            sw = _switches[st.StopSwitch];

                        //create new PdStepper and add it
                        var stepper = new PdStepper(this, st.Name, st.BoardId,
                        st.IsStepper1 ? (byte)1 : (byte)0, st.Speed, sw);

                        _steppers.Add(i, st.Name, stepper);
                        i++;
                    }                    
                }
            }

            //process and add servos
            if (g_machineType == MachineType.PDB && _servos != null)
            {
                ushort i = 0;
                foreach (ServoConfigFileEntry sv in config.PRServos)
                {
                    if (sv.IsEnabled)
                    {
                        var servo = new PdServo(this, sv.Name, sv.BoardId, sv.Index, sv.MinValue);
                        _servos.Add(i, sv.Name, servo);
                        i++;
                    }
                }
            }

            if(g_machineType == MachineType.PDB && _serialLeds != null)
            {
                ushort i = 0;
                foreach (WS281xConfigFileEntry sv in config.PRWs281x)
                {
                    if (sv.IsEnabled)
                    {
                        var serialLed = new PdSerialLed(this, sv.Name, PdSerialLedType.WS2811, sv.BoardId, (uint)sv.Index, sv.First, sv.Last);
                        _serialLeds.Add(i, sv.Name, serialLed);
                        i++;
                    }
                }

                i = 0;
                foreach (Lpd8806ConfigFileEntry sv in config.PRLpd8806)
                {
                    if (sv.IsEnabled)
                    {
                        var serialLed = new PdSerialLed(this, sv.Name, PdSerialLedType.Lpd8806, sv.BoardId, (uint)sv.Index, sv.First, sv.Last);
                        _serialLeds.Add(i, sv.Name, serialLed);
                        i++;
                    }
                }
            }

            //register indirect addresses for the PDLed boards added from LEDS, Steppers
            PdLeds.PDLEDS.ForEach(x =>
            {
                if(_steppers != null)
                {
                    //Setup any Steppers assigned to this board, then update the ControlRegister if any available
                    var steppers = _steppers?.Values?.Where(st => st.BoardAddress == x.BoardAddress);
                    foreach (var stepper in steppers)
                    {
                        if (stepper.StepperIndex == 0)
                            x.ControlRegister.UseStepper0 = 1;
                        else x.ControlRegister.UseStepper1 = 1;
                    }
                }

                //Setup any Servos assigned to this board, then update the ServoRegister if any available
                var servos = _servos?.Values?.Where(sv => sv.BoardId == x.BoardAddress);
                if(servos != null)
                {
                    foreach (var servo in servos)
                    {
                        switch (servo.ServoIndex)
                        {
                            case 0: x.ServoRegister.UseServo0 = 1; break;
                            case 1: x.ServoRegister.UseServo1 = 1; break;
                            case 2: x.ServoRegister.UseServo2 = 1; break;
                            case 3: x.ServoRegister.UseServo3 = 1; break;
                            case 4: x.ServoRegister.UseServo4 = 1; break;
                            case 5: x.ServoRegister.UseServo5 = 1; break;
                            case 6: x.ServoRegister.UseServo6 = 1; break;
                            case 7: x.ServoRegister.UseServo7 = 1; break;
                            case 8: x.ServoRegister.UseServo8 = 1; break;
                            case 9: x.ServoRegister.UseServo9 = 1; break;
                            case 10: x.ServoRegister.UseServo10 = 1; break;
                            case 11: x.ServoRegister.UseServo11 = 1; break;
                            default:
                                break;
                        }
                    }
                }                

                //SERIAL LEDS - TODO: not use these default bit times?
                x.WriteWS281xControlRegister();

                //run through any enabled serial leds that were found, enable in control register and set first and last addresses
                var serialLeds = _serialLeds?.Values.Where(sled => sled.BoardId == x.BoardAddress);
                if(serialLeds?.Any() ?? false)
                {
                    foreach (var serialLed in serialLeds)
                    {
                        if (serialLed.PdSerialLedType == PdSerialLedType.WS2811)
                        {
                            switch (serialLed.Index)
                            {
                                case 0: x.ControlRegister.UseWS281x0 = 1; break;
                                case 1: x.ControlRegister.UseWS281x1 = 1; break;
                                case 2: x.ControlRegister.UseWS281x2 = 1; break;
                                default: break;
                            }

                            x.WriteWS281xRangeRegister(serialLed.Index, serialLed.First, serialLed.Last);
                        }
                        else if (serialLed.PdSerialLedType == PdSerialLedType.Lpd8806)
                        {
                            switch (serialLed.Index)
                            {
                                case 0: x.ControlRegister.UseLpd880x0 = 1; break;
                                case 1: x.ControlRegister.UseLpd880x1 = 1; break;
                                case 2: x.ControlRegister.UseLpd880x2 = 1; break;
                                default: break;
                            }

                            x.WriteLpd8806RangeRegister(serialLed.Index, serialLed.First, serialLed.Last);
                        }
                    }
                }
                

                //use controls see ControlRegister options
                x.WriteControlRegister();

                //todo: servoMaxValue config
                x.WriteServoRegister(500);
            });            

            if (_gi != null)
            {
                foreach (GIConfigFileEntry ge in config.PRGI)
                {
                    Driver d = new (this, ge.Name, PinProc.PRDecode(g_machineType, ge.Number));
                    Logger?.Log("Adding GI " + d.ToString(), LogLevel.Verbose);
                    _gi.Add(d.Number, d.Name, d);
                }
            }

            foreach (VirtualDriver virtual_driver in new_virtual_drivers)
            {
                int base_group_number = virtual_driver.Number / 8;
                List<Driver> items_to_remove = new();
                foreach (Driver d in (_coils?.Values).Cast<Driver>())
                {
                    if (d.Number / 8 == base_group_number)
                        items_to_remove.Add(d);
                }
                foreach (Driver d in items_to_remove)
                {
                    _coils.Remove(d.Name);
                    VirtualDriver vd = new(this, d.Name, d.Number, polarity);
                    _coils.Add(d.Number, d.Name, d);
                }
                items_to_remove.Clear();
                foreach (Driver d in (_lamps?.Values).Cast<Driver>())
                {
                    if (d.Number / 8 == base_group_number)
                        items_to_remove.Add(d);
                }
                foreach (Driver d in items_to_remove)
                {
                    _lamps.Remove(d.Name);
                    VirtualDriver vd = new(this, d.Name, d.Number, polarity);
                    _lamps.Add(d.Number, d.Name, d);
                }
            }
        }

        /// <summary>
        /// See <see cref="PinProc.PRSwitchGetStates(IntPtr, EventType[], ushort)"/>
        /// </summary>
        /// <returns></returns>
        public EventType[] SwitchGetStates()
        {
            ushort numSwitches = PinProc.kPRSwitchPhysicalLast + 1;
            EventType[] procSwitchStates = new EventType[numSwitches];
            lock (procSyncObject)
            {
                PinProc.PRSwitchGetStates(ProcHandle, procSwitchStates, numSwitches);
            }
            return procSwitchStates;
        }

        /// <summary>
        /// See <see cref="PinProc.PRSwitchUpdateConfig(IntPtr, ref SwitchConfig)"/>
        /// </summary>
        /// <param name="number"></param>
        /// <param name="event_type"></param>
        /// <param name="rule"></param>
        /// <param name="linked_drivers"></param>
        /// <param name="drive_outputs_now"></param>
        public void SwitchUpdateRule(ushort number, EventType event_type, SwitchRule rule, DriverState[] linked_drivers, bool drive_outputs_now)
        {
            int numDrivers = 0;
            if (linked_drivers != null)
                numDrivers = linked_drivers.Length;

            bool use_column_8 = g_machineType == MachineType.WPC;

            if (firstTime)
            {
                firstTime = false;
                SwitchConfig switchConfig = new()
                {
                    Clear = false,
                    UseColumn8 = use_column_8,
                    UseColumn9 = false, // No WPC machines actually use this
                    HostEventsEnable = true,
                    DirectMatrixScanLoopTime = 2, // Milliseconds
                    PulsesBeforeCheckingRX = 10,
                    InactivePulsesAfterBurst = 12,
                    PulsesPerBurst = 6,
                    PulseHalfPeriodTime = 13 // Milliseconds
                };
                lock (procSyncObject)
                {
                    PinProc.PRSwitchUpdateConfig(ProcHandle, ref switchConfig);
                }
            }
            Result r;
            lock (procSyncObject)
            {
                r = PinProc.PRSwitchUpdateRule(ProcHandle, (byte)number, event_type, ref rule, linked_drivers, numDrivers, drive_outputs_now);
            }
            if (r == Result.Success)
            {
                // Possibly we should Flush the write data here
            }
            else
            {
                Logger.Log(String.Format("SwitchUpdateRule FAILED for #{0} event_type={1} numDrivers={2} drive_outputs_now={3}",
                    number, event_type.ToString(), numDrivers, drive_outputs_now), LogLevel.Error);
            }
        }

        /// <summary>
        /// See <see cref="PinProc.PRDriverWatchdogTickle(IntPtr)"/> and FlushesWriteData
        /// </summary>
        public void WatchDogTickle()
        {
            lock (procSyncObject)
            {
                PinProc.PRDriverWatchdogTickle(ProcHandle);
                PinProc.PRFlushWriteData(ProcHandle);
            }
        }

        /// <summary>
        /// See <see cref="PinProc.PRWriteData(IntPtr, uint, uint, int, ref uint)"/>
        /// </summary>
        /// <param name="module"></param>
        /// <param name="startingAddr"></param>
        /// <param name="data"></param>
        /// <returns></returns>
		public Result WriteData(uint module, uint startingAddr, ref uint data)
		{
			Result r;
			lock (procSyncObject) {
				//Logger.Log(String.Format("write_data - thread: {0}", System.Threading.Thread.CurrentThread.Name));

				r = PinProc.PRWriteData(ProcHandle, module, startingAddr, 1, ref data);
				//Logger.Log(String.Format ("write_data module: {0} start_addr: {1} data: {2}",
				//                          module, startingAddr, data));
			}
			return r;
		}

        private void DMDConfigPopulateDefaults(ref DMDConfig dmdConfig)
        {
            dmdConfig.EnableFrameEvents = true;
            dmdConfig.NumRows = kDMDRows;
            dmdConfig.NumColumns = kDMDColumns;
            dmdConfig.NumSubFrames = kDMDSubFrames;
            dmdConfig.NumFrameBuffers = kDMDFrameBuffers;
            dmdConfig.AutoIncBufferWrPtr = true;

            for (int i = 0; i < dmdConfig.NumSubFrames; i++)
            {
                dmdConfig.RclkLowCycles[i] = 15;
                dmdConfig.LatchHighCycles[i] = 15;
                dmdConfig.DotclkHalfPeriod[i] = 1;
            }
            dmdConfig.DeHighCycles[0] = 90;
            dmdConfig.DeHighCycles[1] = 190;
            dmdConfig.DeHighCycles[2] = 50;
            dmdConfig.DeHighCycles[3] = 377;
        }

        private IntPtr pinprocNativeLib;

        /// <summary> Load the correct DLL depending on bitness of machine Any/32/64 </summary>
        private void NativeLoadLibPinProcDll()
        {
            var path = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule?.FileName);
            var fullPath = string.Empty;

            //set path from diff OS and 64bit process
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                fullPath = Environment.Is64BitProcess ? Path.Combine(path, @"lib\x64\libpinproc.dll") : Path.Combine(path, @"lib\x86\libpinproc.dll");
            else if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                fullPath = Environment.Is64BitProcess ? Path.Combine(path, @"lib\x64\libpinproc.x64.so") : Path.Combine(path, @"lib\x86\libpinproc.so");
            else if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                fullPath = Environment.Is64BitProcess ? Path.Combine(path, @"lib\x64\libpinproc.dylib") : string.Empty;
            
            //load the native lib
            pinprocNativeLib = NativeLibrary.Load(fullPath);
        }

        bool disposed = false;
        /// <summary> Free native DLL </summary>
        public void Dispose()
        {
            if (!disposed && pinprocNativeLib != IntPtr.Zero)
            {
                NativeLibrary.Free(pinprocNativeLib);
                disposed = true;
            }                
        }
    }
}

