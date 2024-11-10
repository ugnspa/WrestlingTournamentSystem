using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrestlingTournamentSystem.DataAccess.Entities
{
    public class Session
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string LastRefreshToken { get; set; } = null!;

        [Required]
        public DateTimeOffset InitiatedAt { get; set; }

        [Required]
        public DateTimeOffset ExpiresAt { get; set; }

        [Required]
        public bool IsRevoked { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public User User { get; set; } = null!;
    }
}
