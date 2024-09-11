using Microsoft.AspNetCore.Mvc;
using NetPinProc.Game.Manager.Server.Controllers.Base;
using NetPinProc.Game.Sqlite.Model;

namespace NetPinProc.Game.Manager.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesPlayedController : EntityBaseController<GamePlayed>
    {

    }
}
