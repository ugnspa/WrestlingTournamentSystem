using Microsoft.AspNetCore.Mvc;
using WrestlingTournamentSystem.DataAccess.Helpers;
using WrestlingTournamentSystem.DataAccess.Helpers.Exceptions;

namespace WrestlingTournamentSystem.Api.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult HandleException(Exception ex)
        {
            var statusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                BusinessRuleValidationException => StatusCodes.Status422UnprocessableEntity,
                _ => StatusCodes.Status500InternalServerError
            };

            var title = statusCode == StatusCodes.Status500InternalServerError ? "Internal Server Error" : ex.Message;

            return StatusCode(statusCode, new ErrorResponse(statusCode, title));
        }
    }
}
