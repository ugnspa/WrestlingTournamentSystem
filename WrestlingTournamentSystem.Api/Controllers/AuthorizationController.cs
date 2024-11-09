using Microsoft.AspNetCore.Mvc;

namespace WrestlingTournamentSystem.Api.Controllers
{
    public class AuthorizationController : ControllerBase
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
