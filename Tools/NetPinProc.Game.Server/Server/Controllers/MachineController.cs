using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetPinProc.Domain.Constants;
using NetPinProc.Game.Sqlite;
using NetPinProc.Game.Sqlite.Model;

namespace NetPinProc.Game.Manager.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MachineController : NetProcBaseController
    {
        public MachineController(
            ILogger<MachineController> logger,
            INetProcDbContext netProcDb) :
            base(logger, netProcDb)
        { }

        /// <summary>Gets the first machine row</summary>
        /// <returns></returns>
        public async Task<Machine> OnGetAsync() =>
            await _netProcDb.Machine.FirstAsync();

        /// <summary>Just a quick bog standard text print of all items in the machine</summary>
        /// <returns></returns>
        [HttpGet("MachineItemListText")]
        public async Task<string> GetMachineItemListsAsync()
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