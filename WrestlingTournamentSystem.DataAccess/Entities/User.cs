using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public ICollection<Tournament> Tournaments { get; set; } = new List<Tournament>();
        public ICollection<Wrestler> Wrestlers { get; set; } = new List<Wrestler>();
        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
