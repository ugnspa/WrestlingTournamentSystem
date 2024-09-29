using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrestlingTournamentSystem.BusinessLogic.Validation
{
    public interface IValidationService
    {
        void ValidateBirthDate(DateTime? birthDate);
    }
}
