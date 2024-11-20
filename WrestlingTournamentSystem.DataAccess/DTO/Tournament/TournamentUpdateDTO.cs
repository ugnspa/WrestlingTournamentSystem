using System.ComponentModel.DataAnnotations;

namespace WrestlingTournamentSystem.DataAccess.DTO.Tournament
{
    public class TournamentUpdateDTO
    {
        [StringLength(100)]
        [Required]
        public string Name { get; set; } = null!;

        [StringLength(255)]
        [Required]
        public string Location { get; set; } = null!;

        [Required]
        public DateTime? StartDate { get; set; }

        [Required]
        public DateTime? EndDate { get; set; }

        [Required]
        public int StatusId { get; set; }
    }
}
