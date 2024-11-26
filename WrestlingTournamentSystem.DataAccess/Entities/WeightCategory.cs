using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrestlingTournamentSystem.DataAccess.Entities
{
    public class WeightCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int Weight { get; set; }

        [Required]
        [StringLength(20)]
        public string Age { get; set; } = null!;

        [Required]
        public bool PrimaryCategory { get; set; }

        // Navigation properties
        [Required]
        public int StyleId { get; set; }
        public WrestlingStyle WrestlingStyle { get; set; } = null!;
    }
}
