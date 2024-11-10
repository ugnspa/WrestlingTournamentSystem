using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
