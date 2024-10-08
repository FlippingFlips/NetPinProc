﻿using NetPinProc.Domain;
using NetPinProc.Domain.PinProc;
using System.Threading;

namespace NetPinProc.Tests
{
    /// <summary>With helper methods for initial connection and loading configuration</summary>
    public abstract class ProcDeviceTestBase
    {
        public IProcDevice? PROC;
        protected MachineType MACHINE_TYPE = MachineType.PDB;
        public const byte PROC_OUTPUT_MODULE = 0;
        public const int PROC_PDB_BUS_ADDRESS = 0xC00;
        protected CancellationTokenSource CancelSource = new();

        protected void InitPRCODeviceAndReset()
        {
            PROC = new ProcDevice(MACHINE_TYPE);
            PROC.Reset(1);
        }

        protected MachineConfiguration LoadMachineConfigFile()
        {
            var config = MachineConfiguration.FromFile("Configs/machine.json");
            MACHINE_TYPE = config.PRGame.MachineType;
            return config;
        }
    }
}
