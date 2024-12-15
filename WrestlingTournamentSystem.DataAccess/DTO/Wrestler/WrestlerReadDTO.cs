using WrestlingTournamentSystem.DataAccess.Entities;

namespace WrestlingTournamentSystem.DataAccess.DTO.Wrestler
{
    public class WrestlerReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Country { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string? PhotoUrl { get; set; }
        public WrestlingStyle WrestlingStyle { get; set; } = null!;
        public string? CoachName { get; set; }
        public string? CoachId { get; set; }
    }
}
