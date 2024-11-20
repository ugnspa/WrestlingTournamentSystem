namespace WrestlingTournamentSystem.DataAccess.Response
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public object? Data { get; set; } = null;

        public List<string>? Errors { get; set; } = null;

        public ApiResponse(bool success, string status, string message, object? data , List<string>? errors )
        {
            Success = success;
            Status = status;
            Message = message;
            Data = data;
            Errors = errors;
        }

        public static ApiResponse SuccessResponse(string message, object? data = null)
        {
            return new ApiResponse(true, "200", message, data, null);
        }

        public static ApiResponse ErrorResponse(string message, List<string> errors)
        {
            return new ApiResponse(false, "400", message, null, errors);
        }

        public static ApiResponse NotFoundResponse(string message)
        {
            return new ApiResponse(false, "404", message, null, null);
        }

        public static ApiResponse InternalServerErrorResponse(string message)
        {
            return new ApiResponse(false, "500", message, null, null);
        }

        public static ApiResponse UnprocessableEntityResponse(string message, List<string> errors)
        {
            return new ApiResponse(false, "422", message, null, errors);
        }

        public static ApiResponse ForbiddenResponse(string message)
        {
            return new ApiResponse(false, "403", message, null, null);
        }
        public static ApiResponse UnauthorizedResponse(string message)
        {
            return new ApiResponse(false, "401", message, null, null);
        }
    }
}
