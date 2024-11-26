namespace WrestlingTournamentSystem.DataAccess.DTO.User
{
    public class AccessTokenDto(string accessToken)
    {
        public string AccessToken { get; set; } = accessToken;
    }
}
