using System.Text.Json.Serialization;

namespace WrestlingTournamentSystem.DataAccess.Helpers.Responses
{
    public class ApiResponse(bool success, int status, string message, object? data, List<string>? errors)
    {
        public bool Success { get; set; } = success;
        public int Status { get; set; } = status;
        public string Message { get; set; } = message;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object? Data { get; set; } = data;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? Errors { get; set; } = errors;

        public static ApiResponse OkResponse(string message, object? data = null)
        {
            return new ApiResponse(true, 200, message, data, null);
        }

        public static ApiResponse CreatedResponse(string message, object? data)
        {
            return new ApiResponse(true, 201, message, data, null);
        }

        public static ApiResponse NoContentResponse()
        {
            return new ApiResponse(true, 204, "No content", null, null);
        }

        public static ApiResponse ErrorResponse(string message, List<string> errors)
        {
            return new ApiResponse(false, 400, message, null, errors);
        }

        public static ApiResponse NotFoundResponse(string message)
        {
            return new ApiResponse(false, 404, message, null, null);
        }

        public static ApiResponse InternalServerErrorResponse(string message)
        {
            return new ApiResponse(false, 500, message, null, null);
        }

        public static ApiResponse UnprocessableEntityResponse(string message)
        {
            return new ApiResponse(false, 422, message, null, null);
        }

        public static ApiResponse ForbiddenResponse(string message)
        {
            return new ApiResponse(false, 403, message, null, null);
        }
        public static ApiResponse UnauthorizedResponse(string message)
        {
            return new ApiResponse(false, 401, message, null, null);
        }
    }
}
