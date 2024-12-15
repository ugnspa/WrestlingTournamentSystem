using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WrestlingTournamentSystem.DataAccess.Entities;

namespace WrestlingTournamentSystem.DataAccess.DTO.WeightCategory
{
    public class WeightCategoryReadDto
    {
        public int Id { get; set; }
        public int Weight { get; set; }
        public string Age { get; set; } = null!;
        public bool PrimaryCategory { get; set; }
        public WrestlingStyle WrestlingStyle { get; set; } = null!;
    }
}
