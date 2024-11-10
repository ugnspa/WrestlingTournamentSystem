﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrestlingTournamentSystem.DataAccess.DTO.User
{
    public class SuccessfulLoginDTO
    {
        public string UserId { get; set; }
        public string AccessToken { get; set; }

        public SuccessfulLoginDTO(string userId, string accessToken)
        {
            UserId = userId;
            AccessToken = accessToken;
        }
    }
}
