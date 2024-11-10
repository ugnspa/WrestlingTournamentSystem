using Microsoft.AspNetCore.Mvc;
using WrestlingTournamentSystem.DataAccess.Helpers.Exceptions;
using WrestlingTournamentSystem.DataAccess.Helpers.Responses;

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
                ForbiddenException => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError
            };

            var title = statusCode == StatusCodes.Status500InternalServerError ? "Internal Server Error" : ex.Message;

            return StatusCode(statusCode, new ErrorResponse(statusCode, title));
        }
    }
}
