using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetPinProc.Game.Sqlite;
using NetPinProc.Game.Sqlite.Model;
using System;

namespace NetPinProc.Game.Manager.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MediaController : ControllerBase
    {
        public MediaController(ILogger<MediaController> logger) { }

        [HttpGet("{name}")]
        public async Task<ActionResult<Media>> OnGetByNameAsync(
            [FromServices] INetProcDbContext netProcDbContext,
            string name)
        {
            return Ok(await netProcDbContext.Media.FirstOrDefaultAsync(x => x.Name == name));
        }

        [HttpPost("{name}")]
        public async Task<ActionResult<Media>> OnPostByNameAsync(
            [FromServices] INetProcDbContext netProcDbContext,
            string name)
        {
            if (HttpContext.Request.Form.Files.Any())
            {
                foreach (var file in HttpContext.Request.Form.Files)
                {
                    using var ms = new MemoryStream();
                    await file.CopyToAsync(ms);
                    var data = ms.ToArray();

                    var media = await netProcDbContext.Media.FirstOrDefaultAsync(x => x.Name == name);
                    if (media != null)
                    {                        
                        media.Data = data;
                        media.Size = data.Length;
                        netProcDbContext.Media.Update(media);
                        await netProcDbContext.SaveChangesAsync();
                        return Ok(media);
                    }
                    else
                    {
                        media = new();
                        media.Data = data;
                        media.Size = data.Length;
                        media.MimeType = file.ContentType;
                        media.Name = file.FileName;
                        netProcDbContext.Media.Add(media);
                        await netProcDbContext.SaveChangesAsync();
                        return Ok(media);
                    }
                }
            }


            return BadRequest();
        }
    }
}
