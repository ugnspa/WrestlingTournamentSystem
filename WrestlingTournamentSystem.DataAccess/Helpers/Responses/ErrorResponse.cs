namespace WrestlingTournamentSystem.DataAccess.Helpers.Responses
{
    public class ErrorResponse
    {
        public int Status { get; set; }
        public string Title { get; set; }

        public ErrorResponse(int status, string title)
        {
            Status = status;
            Title = title;
        }
    }
}
