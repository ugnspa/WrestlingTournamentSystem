﻿namespace WrestlingTournamentSystem.DataAccess.DTO.Tournament
{
    public class TournamentReadDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Location { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Status { get; set; } = null!;

    }
}
