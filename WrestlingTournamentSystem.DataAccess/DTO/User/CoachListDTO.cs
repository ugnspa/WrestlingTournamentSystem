using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WrestlingTournamentSystem.DataAccess.DTO.Wrestler;

namespace WrestlingTournamentSystem.DataAccess.DTO.User
{
    public class CoachListDTO
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
