using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrestlingTournamentSystem.DataAccess.Entities
{
    public class Tournament
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(255)]
        public string Location { get; set; } = null!;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        // Navigation properties
        [Required]
        public string OrganiserId { get; set; } = null!;
        public User Organiser { get; set; } = null!;


        [Required]
        public int StatusId { get; set; }
        public TournamentStatus TournamentStatus { get; set; } = null!;

        public ICollection<TournamentWeightCategory>? TournamentWeightCategories { get; set; }
    }
}
