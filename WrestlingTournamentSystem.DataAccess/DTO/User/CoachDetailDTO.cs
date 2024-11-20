﻿using WrestlingTournamentSystem.DataAccess.DTO.Wrestler;

namespace WrestlingTournamentSystem.DataAccess.DTO.User
{
    public class CoachDetailDTO
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Email { get; set; } = null!;
        public List<WrestlerReadDTO>? Wrestlers { get; set; }
    }
}
