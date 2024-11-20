
namespace WrestlingTournamentSystem.DataAccess.DTO.TournamentWeightCategory
{
    public class TournamentWeightCategoryReadDTO
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Weight { get; set; }
        public string Age { get; set; } = null!;
        public string Style { get; set; } = null!;
        public string Status { get; set; } = null!;
    }
}
