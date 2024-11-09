using Microsoft.AspNetCore.Mvc;
using WrestlingTournamentSystem.DataAccess.Exceptions;

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

            var respone = new
            {
                status = statusCode,
                title = statusCode == StatusCodes.Status500InternalServerError ? "Internal Server Error" : ex.Message
            };

            return StatusCode(statusCode, respone);
        }
    }
}
