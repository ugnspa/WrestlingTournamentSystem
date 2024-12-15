using System.ComponentModel.DataAnnotations;

namespace WrestlingTournamentSystem.DataAccess.DTO.TournamentWeightCategory
{
    public class TournamentWeightCategoryUpdateDto
    {
        [Required]
        public int StatusId { get; set; }

    }
}
