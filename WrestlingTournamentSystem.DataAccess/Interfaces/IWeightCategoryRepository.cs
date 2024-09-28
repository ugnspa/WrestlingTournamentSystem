﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrestlingTournamentSystem.DataAccess.Interfaces
{
    public interface IWeightCategoryRepository
    {
        public Task<bool> WeightCategoryExistsAsync(int WeigthCategoryId);
    }
}