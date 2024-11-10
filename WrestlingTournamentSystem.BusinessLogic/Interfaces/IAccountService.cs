using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WrestlingTournamentSystem.DataAccess.DTO.User;

namespace WrestlingTournamentSystem.BusinessLogic.Interfaces
{
    public interface IAccountService
    {
        public Task Register(RegisterUserDTO registerUserDTO);
        public Task<SuccessfulLoginDTO> Login(LoginUserDTO loginUserDTO);
        public Task<string> CreateRefreshToken(string userId);
        public Task<SuccessfulLoginDTO> GetAccessTokenFromRefreshToken(string? refreshToken);
    }
}
