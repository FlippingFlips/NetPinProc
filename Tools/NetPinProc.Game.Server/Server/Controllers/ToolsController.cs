using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetPinProc.Domain.Constants;
using NetPinProc.Domain.MachineConfig;
using NetPinProc.Game.Manager.Shared.Dto;
using NetPinProc.Game.Manager.Shared.Tools.Playfield;
using NetPinProc.Game.Sqlite;
using System.Text.Json;

namespace NetPinProc.Game.Manager.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToolsController : ControllerBase
    {
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpPost("ExtractFromSvg")]
        public async Task<ActionResult<string>> OnExtractFromSvgAsync(
            [FromServices] INetProcDbContext context,
            [FromBody] string template)
        {
            var machineDict = await GetMachineItemCollection(context);

            machineDict = InkscapeSvg.ExtractPositions(template, machineDict);

            context.Switches.UpdateRange(machineDict[Names.SWITCHES].Cast<SwitchConfigFileEntry>());
            context.Coils.UpdateRange(machineDict[Names.DRIVERS].Cast<CoilConfigFileEntry>());
            context.Lamps.UpdateRange(machineDict[Names.LAMPS].Cast<LampConfigFileEntry>());
            context.Leds.UpdateRange(machineDict[Names.LEDS].Cast<LedConfigFileEntry>());
            context.Servos.UpdateRange(machineDict[Names.SERVOS].Cast<ServoConfigFileEntry>());
            context.Steppers.UpdateRange(machineDict[Names.STEPPERS].Cast<StepperConfigFileEntry>());

            await context.SaveChangesAsync();

            return Ok();
        }

        /// <summary>generates svg from a given template with machine collections</summary>
        /// <param name="context"></param>
        /// <param name="svgDto"></param>
        /// <returns>Base64 string</returns>
        [HttpPost("GenerateFromSvg")]
        public async Task<ActionResult<string>> OnGenerateFromSvgAsync(
            [FromServices] INetProcDbContext context,
            [FromBody] SvgDto svgDto)
        {
            var machineDict = await GetMachineItemCollection(context);
            
            var result = InkscapeSvg
                .GenerateFromTemplate(svgDto, machineDict);

            //convert the stream to string
            result.Position = 0;
            var buffer = new byte[result.Length];
            result.Read(buffer, 0, buffer.Length);
            result.Dispose();

            //return it
            return Convert.ToBase64String(buffer);
        }

        [HttpGet("ExportToJson")]
        public async Task<ActionResult<string>> OnGetMachineExportToJsonAsync(
            [FromServices] INetProcDbContext context)
        {
            var machineConfig = context.GetMachineConfiguration();
            return JsonSerializer.Serialize(machineConfig, options: new JsonSerializerOptions
            {
                 WriteIndented = true,
            });
        }

        [HttpGet("Test")]
        public async Task<ActionResult> OnTestPaths()
        {            
            var path = Directory.GetCurrentDirectory();
            path = Path.Combine(path, @"../Client/wwwroot/playfield");

            if (Directory.Exists(path))
                ;
            
            return Ok();
        }


        private static async Task<Dictionary<string, IEnumerable<ConfigFileEntryBase>>> GetMachineItemCollection(INetProcDbContext context)
        {
            //get all machine items
            var switches = await context.Switches.AsNoTracking().ToListAsync();
            var lamps = await context.Lamps.AsNoTracking().ToListAsync();
            var leds = await context.Leds.AsNoTracking().ToListAsync();
            var drivers = await context.Coils.AsNoTracking().ToListAsync();
            var steppers = await context.Steppers.AsNoTracking().ToListAsync();
            var servos = await context.Servos.AsNoTracking().ToListAsync();
            var gi = await context.GI.AsNoTracking().ToListAsync();

            var machineDict = new Dictionary<string, IEnumerable<Domain.MachineConfig.ConfigFileEntryBase>>
                {
                    { Names.SWITCHES, switches },
                    { Names.LAMPS, lamps },
                    { Names.LEDS, leds },
                    { Names.DRIVERS, drivers },
                    { Names.STEPPERS, steppers },
                    { Names.SERVOS, servos },
                    { Names.GI, gi }
                };
            return machineDict;
        }
    }
}
