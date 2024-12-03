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
        public string Style { get; set; } = null!;
        public string? Coach { get; set; }
    }
}
