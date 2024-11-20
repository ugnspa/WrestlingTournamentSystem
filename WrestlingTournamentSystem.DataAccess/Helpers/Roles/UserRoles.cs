namespace WrestlingTournamentSystem.DataAccess.Helpers.Roles
{
    public class UserRoles
    {
        public const string Admin = nameof(Admin);
        public const string Coach = nameof(Coach);
        public const string TournamentOrganiser = nameof(TournamentOrganiser);

        public static readonly IReadOnlyCollection<string> All = [
            Admin,
            Coach,
            TournamentOrganiser
            ];
    }
}
