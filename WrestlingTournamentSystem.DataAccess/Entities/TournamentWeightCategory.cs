using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrestlingTournamentSystem.DataAccess.Entities
{
    public class TournamentWeightCategory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        public DateOnly StartDate { get; set; }

        [Required]
        public DateOnly EndDate { get; set; }

        // Navigation properties

        [Required]
        public int StatusId { get; set; }
        public TournamentWeightCategoryStatus TournamentWeightCategoryStatus { get; set; } = null!;

        [Required]
        public int fk_TournamentId { get; set; }
        public Tournament Tournament { get; set; } = null!;

        [Required]
        public int fk_WeightCategoryId { get; set; }
        public WeightCategory WeightCategory { get; set; } = null!;

        public ICollection<Wrestler> Wrestlers { get; set; } = new List<Wrestler>();
    }
}
