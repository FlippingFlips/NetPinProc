using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetPinProc.Domain;
using NetPinProc.Domain.Constants;
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

        /// <summary>Gets the first machine row</summary>
        /// <returns></returns>
        public async Task<Machine> OnGetAsync(
            [FromServices] INetProcDbContext context) =>
            await context.Machine.FirstAsync();

        [HttpPut]
        public async Task<Machine> OnPutAsync(
            [FromServices] NetProcDbContext context,
            Machine machine)
        {
            context.Update(machine);
            await context.SaveChangesAsync();
            return machine;
        }
            
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
                .Select(x=>x.Name).ToListAsync();
            var lamps = await _netProcDb.Lamps.AsNoTracking().Select(x => x.Name).ToListAsync();
            var leds = await _netProcDb.Leds.AsNoTracking().Select(x => x.Name).ToListAsync();
            var drivers = await _netProcDb.Coils.AsNoTracking().Select(x => x.Name).ToListAsync();
            var steppers = await _netProcDb.Steppers.AsNoTracking().Select(x => x.Name).ToListAsync();
            var servos = await _netProcDb.Servos.AsNoTracking().Select(x => x.Name).ToListAsync();
            var gi = await _netProcDb.GI.AsNoTracking().Select(x => x.Name).ToListAsync();

            var result = string.Empty;
            if(switches != null)
            {
                result += $"\n{Names.SWITCHES}\n";
                foreach(var mi in switches) { result += $"{mi}\n"; }
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
    }
}