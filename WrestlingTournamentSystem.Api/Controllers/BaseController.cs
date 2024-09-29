using Microsoft.AspNetCore.Mvc;
using WrestlingTournamentSystem.DataAccess.Exceptions;

namespace WrestlingTournamentSystem.Api.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult HandleException(Exception ex)
        {
            return ex switch
            {
                NotFoundException => NotFound(ex.Message),
                BusinessRuleValidationException => UnprocessableEntity(ex.Message),
                _ => StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error: {ex.Message}")
            };
        }
    }
}
