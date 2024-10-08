﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrestlingTournamentSystem.DataAccess.DTO.TournamentWeightCategory
{
    public class TournamentWeightCategoryUpdateDTO
    {
        [Required]
        public DateTime? StartDate { get; set; }

        [Required]
        public DateTime? EndDate { get; set; }

        [Required]
        public int StatusId { get; set; }

        [Required]
        public int fk_WeightCategoryId { get; set; }
    }
}
