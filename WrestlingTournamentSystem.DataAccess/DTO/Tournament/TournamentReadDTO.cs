﻿using WrestlingTournamentSystem.DataAccess.Entities;

namespace WrestlingTournamentSystem.DataAccess.DTO.Tournament
{
    public class TournamentReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Location { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public TournamentStatus TournamentStatus { get; set; } = null!;

        public string OrganiserId { get; set; } = null!;

    }
}
