using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WrestlingTournamentSystem.DataAccess.Entities
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Surname { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string City { get; set; } = null!;

        //navigation properties 
        public ICollection<Tournament>? Tournaments { get; set; }
        public ICollection<Wrestler>? Wrestlers { get; set; }
        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
