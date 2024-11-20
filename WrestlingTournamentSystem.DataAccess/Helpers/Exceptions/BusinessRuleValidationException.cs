namespace WrestlingTournamentSystem.DataAccess.Helpers.Exceptions
{
    public class BusinessRuleValidationException : Exception
    {
        public BusinessRuleValidationException(string message) : base(message)
        {
        }
    }
}
