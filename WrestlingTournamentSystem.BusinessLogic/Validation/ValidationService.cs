using WrestlingTournamentSystem.DataAccess.Helpers.Exceptions;

namespace WrestlingTournamentSystem.BusinessLogic.Validation
{
    public class ValidationService : IValidationService
    {
        public void ValidateBirthDate(DateTime? birthDate)
        {
            if(birthDate == null)
            {
                throw new BusinessRuleValidationException("Birth date is required.");
            }

            if (birthDate > DateTime.Now)
            {
                throw new BusinessRuleValidationException("Birth date cannot be in the future.");
            }

            if (birthDate < new DateTime(1900, 1, 1))
            {
                throw new BusinessRuleValidationException("Birth date cannot be earlier than January 1, 1900.");
            }
        }

        public void ValidateStartEndDates(DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null)
            {
                throw new BusinessRuleValidationException("Start date is required.");
            }

            if (endDate == null)
            {
                throw new BusinessRuleValidationException("End date is required.");
            }

            if (startDate.Value.Date > endDate.Value.Date)
            {
                throw new BusinessRuleValidationException("Start date cannot be after end date.");
            }
        }

        public void ValidateTournamentWeightCategoryDates(DateTime? tournamentStartDate, DateTime? tournamentEndDate, DateTime? weightCategoryStartDate, DateTime? weightCategoryEndDate)
        {
            if (!tournamentStartDate.HasValue || !tournamentEndDate.HasValue || !weightCategoryStartDate.HasValue || !weightCategoryEndDate.HasValue)
            {
                throw new BusinessRuleValidationException("All dates must be provided.");
            }

            if (weightCategoryStartDate.Value.Date > weightCategoryEndDate.Value.Date)
            {
                throw new BusinessRuleValidationException("Weight category start date must be before end date.");
            }

            if (weightCategoryStartDate.Value.Date < tournamentStartDate.Value.Date || weightCategoryEndDate.Value.Date > tournamentEndDate.Value.Date)
            {
                throw new BusinessRuleValidationException($"Weight category dates must be within the tournament's start ({tournamentStartDate.Value.Date}) and end ({tournamentEndDate.Value.Date}) dates.");
            }
        }

        public void ValidateRegisterPassword(string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                throw new BusinessRuleValidationException("Passwords do not match.");
            }
        }
    }
}
