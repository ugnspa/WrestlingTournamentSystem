using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrestlingTournamentSystem.DataAccess.DTO.Wrestler
{
    public class WrestlerUpdateDTO
    {
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
        public DateTime? BirthDate { get; set; }

        [StringLength(2048)]
        public string? PhotoUrl { get; set; }

        [Required]
        public int StyleId { get; set; }
    }
}
