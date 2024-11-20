namespace WrestlingTournamentSystem.DataAccess.DTO.User
{
    public class AccessTokenDTO
    {
        public string AccessToken { get; set; }
        public AccessTokenDTO(string accessToken)
        {
            AccessToken = accessToken;
        }
    }
}
