using WrestlingTournamentSystem.DataAccess.Entities;


namespace WrestlingTournamentSystem.DataAccess.DTO.TournamentWeightCategory
{
    public class TournamentWeightCategoryReadDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TournamentWeightCategoryStatus TournamentWeightCategoryStatus { get; set; } = null!;
        public Entities.WeightCategory WeightCategory { get; set; } = null!;

    }
}
