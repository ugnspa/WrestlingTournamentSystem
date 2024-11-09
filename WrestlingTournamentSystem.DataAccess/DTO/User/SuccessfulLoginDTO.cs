using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrestlingTournamentSystem.DataAccess.DTO.User
{
    public class SuccessfulLoginDTO
    {
        public string AccessToken { get; set; } = null!;

        public SuccessfulLoginDTO(string accessToken)
        {
            AccessToken = accessToken;
        }
    }
}
