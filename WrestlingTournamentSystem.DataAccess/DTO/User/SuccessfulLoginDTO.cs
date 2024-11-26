namespace WrestlingTournamentSystem.DataAccess.DTO.User
{
    public class SuccessfulLoginDto(string userId, string accessToken)
    {
        public string UserId { get; set; } = userId;
        public string AccessToken { get; set; } = accessToken;
    }
}
