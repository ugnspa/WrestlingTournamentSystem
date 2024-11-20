using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrestlingTournamentSystem.DataAccess.Entities
{
    public class Wrestler
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Surname { get; set; } = null!;

        [Required]
        [StringLength(60)]
        public string Country { get; set; } = null!;

        [Required]
        public DateTime BirthDate { get; set; }

        [StringLength(2048)]
        public string? PhotoUrl { get; set; }

        // Navigation properties
        [Required]
        public int StyleId { get; set; }
        public WrestlingStyle WrestlingStyle { get; set; } = null!;

        public string? CoachId { get; set; }
        public User? Coach { get; set; } = null!;

        public ICollection<TournamentWeightCategory>? TournamentWeightCategories { get; set; }
    }
}
