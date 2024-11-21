using Microsoft.AspNetCore.Mvc;
using WrestlingTournamentSystem.DataAccess.Helpers.Exceptions;
using WrestlingTournamentSystem.DataAccess.Response;

namespace WrestlingTournamentSystem.Api.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult HandleException(Exception ex)
        {
            var message = ex.Message;

            var response = ex switch
            {
                NotFoundException => ApiResponse.NotFoundResponse(message),
                BusinessRuleValidationException => ApiResponse.UnprocessableEntityResponse(message),
                ForbiddenException => ApiResponse.ForbiddenResponse(message),
                _ => ApiResponse.InternalServerErrorResponse("Internal Server Error")
            };

            return StatusCode(response.Status, response);
        }
    }
}
