using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WrestlingTournamentSystem.DataAccess.Exceptions;

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
    }
}
