using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NetPinProc.Domain;
using NetPinProc.Domain.Constants;
using NetPinProc.Game.Manager.Server.Helpers;
using NetPinProc.Game.Sqlite;
using NetPinProc.Game.Sqlite.Model;
using System.Text.Json;

namespace NetPinProc.Game.Manager.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MachineController : ControllerBase
    {
        public MachineController(
            ILogger<MachineController> logger){ }

        /// <summary>Checks compatibility of the machine config with a <see cref="FakePinProc"/></summary>
        /// <param name="netProcDbContext"></param>
        /// <returns></returns>
        [HttpGet("IsCompatible")]
        public async Task<IActionResult> GetIsCompatible(
            [FromServices] INetProcDbContext netProcDbContext)
        {
            try
            {
                var mc = netProcDbContext.GetMachineConfiguration();
                var proc = new FakePinProc(mc.PRGame.MachineType, new ConsoleLogger());
                proc.SetupProcMachine(mc,
                    new AttrCollection<ushort, string, IDriver>(),
                     new AttrCollection<ushort, string, Switch>(),
                     new AttrCollection<ushort, string, IDriver>(),
                     new AttrCollection<ushort, string, LED>(),
                     new AttrCollection<ushort, string, IDriver>(),
                     new AttrCollection<ushort, string, Domain.Pdb.PdStepper>(),
                     new AttrCollection<ushort, string, Domain.Pdb.PdServo>(),
                     new AttrCollection<ushort, string, Domain.Pdb.PdSerialLed>()
                );

                return Ok();
            }
            catch (Exception ex) { return BadRequest($"{ex.Message}\n{ex.StackTrace}"); }
        }

        /// <summary>Just a quick bog standard text print of all items in the machine</summary>
        /// <returns></returns>
        [HttpGet("MachineItemListText")]
        public async Task<string> GetMachineItemListsAsync(
            [FromServices] INetProcDbContext _netProcDb)
        {
            var switches = await _netProcDb.Switches.AsNoTracking()
                .OrderBy(x => x.Number)
                .Select(x => x.Name).ToListAsync();
            var lamps = await _netProcDb.Lamps.AsNoTracking().Select(x => x.Name).ToListAsync();
            var leds = await _netProcDb.Leds.AsNoTracking().Select(x => x.Name).ToListAsync();
            var drivers = await _netProcDb.Coils.AsNoTracking().Select(x => x.Name).ToListAsync();
            var steppers = await _netProcDb.Steppers.AsNoTracking().Select(x => x.Name).ToListAsync();
            var servos = await _netProcDb.Servos.AsNoTracking().Select(x => x.Name).ToListAsync();
            var gi = await _netProcDb.GI.AsNoTracking().Select(x => x.Name).ToListAsync();

            var result = string.Empty;
            if (switches != null)
            {
                result += $"\n{Names.SWITCHES}\n";
                foreach (var mi in switches) { result += $"{mi}\n"; }
            }
            if (lamps != null)
            {
                result += $"\n{Names.LAMPS}\n";
                foreach (var mi in lamps) { result += $"{mi}\n"; }
            }
            if (leds != null)
            {
                result += $"\n{Names.LEDS}\n";
                foreach (var mi in leds) { result += $"{mi}\n"; }
            }
            if (drivers != null)
            {
                result += $"\n{Names.DRIVERS}\n";
                foreach (var mi in drivers) { result += $"{mi}\n"; }
            }
            if (steppers != null)
            {
                result += $"\n{Names.STEPPERS}\n";
                foreach (var mi in steppers) { result += $"{mi}\n"; }
            }
            if (servos != null)
            {
                result += $"\n{Names.SERVOS}\n";
                foreach (var mi in servos) { result += $"{mi}\n"; }
            }
            if (gi != null)
            {
                result += $"\n{Names.GI}\n";
                foreach (var mi in gi) { result += $"{mi}\n"; }
            }

            return result;
        }

        [HttpPost("ImportMachineJson")]
        public async Task<ActionResult<int>> ImportMachineConfig(
                    [FromServices] NetProcDbContext _netProcDb,
                    [FromBody] string machineJson)
        {
            return await ImportJsonToDatabaseAsync(_netProcDb, machineJson);
        }

        [HttpPost("ImportMachineYaml")]
        public async Task<ActionResult<int>> ImportMachineConfigYaml(
                    [FromServices] NetProcDbContext _netProcDb,
                    [FromBody] string machineYaml)
        {
            //convert the yaml to json
            var machineJson = SkeletonGameHelper.ConvertYamlToJson(machineYaml);

            return await ImportJsonToDatabaseAsync(_netProcDb, machineJson);
        }

        /// <summary>Gets the first machine row</summary>
        /// <returns></returns>
        public async Task<Machine> OnGetAsync(
            [FromServices] INetProcDbContext context) =>
            await context.Machine.FirstAsync();

        [HttpGet("ExportToJson")]
        public async Task<ActionResult<string>> OnGetMachineExportToJsonAsync(
            [FromServices] INetProcDbContext context)
        {
            try
            {
                var machineConfig = context.GetMachineConfiguration();

                var machineJson = JsonSerializer.Serialize(machineConfig, options: new JsonSerializerOptions
                {
                    WriteIndented = true,
                });

                return await Task.FromResult(machineJson);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPut]
        public async Task<Machine> OnPutAsync(
            [FromServices] NetProcDbContext context,
            Machine machine)
        {
            context.Update(machine);
            await context.SaveChangesAsync();
            return machine;
        }

        /// <summaryTries to import a machine config json to the database in a transaction</summary>
        /// <param name="_netProcDb"></param>
        /// <param name="machineJson"></param>
        /// <returns></returns>
        private async Task<ActionResult<int>> ImportJsonToDatabaseAsync(
            NetProcDbContext _netProcDb,
            string machineJson)
        {
            using var transaction = _netProcDb.Database.BeginTransaction();
            try
            {
                var config = MachineConfiguration.FromJSON(machineJson);
                Console.WriteLine(config.PRCoils?.Count);

                if (config.PRGame != null)
                {
                    var machine = await _netProcDb.Machine.FirstAsync();
                    machine.NumBalls = config.PRGame.NumBalls;
                    machine.MachineType = config.PRGame.MachineType;
                }

                _netProcDb.Switches.RemoveRange(_netProcDb.Switches);
                _netProcDb.Lamps.RemoveRange(_netProcDb.Lamps);
                _netProcDb.Coils.RemoveRange(_netProcDb.Coils);
                _netProcDb.Leds.RemoveRange(_netProcDb.Leds);
                _netProcDb.Steppers.RemoveRange(_netProcDb.Steppers);
                _netProcDb.WS281xLeds.RemoveRange(_netProcDb.WS281xLeds);
                _netProcDb.Servos.RemoveRange(_netProcDb.Servos);
                _netProcDb.Lpd8806Leds.RemoveRange(_netProcDb.Lpd8806Leds);
                await _netProcDb.SaveChangesAsync();

                if (config?.PRSwitches?.Any() ?? false)
                    _netProcDb.Switches.AddRange(config.PRSwitches);
                if (config?.PRLamps?.Any() ?? false)
                    _netProcDb.Lamps.AddRange(config.PRLamps);
                if (config?.PRCoils?.Any() ?? false)
                    _netProcDb.Coils.AddRange(config.PRCoils);
                if (config?.PRLeds?.Any() ?? false)
                    _netProcDb.Leds.AddRange(config.PRLeds);
                if (config?.PRSteppers?.Any() ?? false)
                    _netProcDb.Steppers.AddRange(config.PRSteppers);
                if (config?.PRServos?.Any() ?? false)
                    _netProcDb.Servos.AddRange(config.PRServos);
                if (config?.PRWs281x?.Any() ?? false)
                    _netProcDb.WS281xLeds.AddRange(config.PRWs281x);
                if (config?.PRLpd8806?.Any() ?? false)
                    _netProcDb.Lpd8806Leds.AddRange(config.PRLpd8806);

                await _netProcDb.SaveChangesAsync();
                await transaction.CommitAsync();

                return 0;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest($"{ex.InnerException?.Message}-{ex.Message}");
            }
        }
    }
}