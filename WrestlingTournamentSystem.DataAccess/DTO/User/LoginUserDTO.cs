using System.ComponentModel.DataAnnotations;

namespace WrestlingTournamentSystem.DataAccess.DTO.User
{
    public class LoginUserDTO
    {
        [Required]
        [StringLength(256)]
        public string UserName { get; set; } = null!;

        [Required]
        [StringLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
