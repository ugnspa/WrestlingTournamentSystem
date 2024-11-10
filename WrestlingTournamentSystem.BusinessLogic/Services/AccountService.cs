﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WrestlingTournamentSystem.BusinessLogic.Interfaces;
using WrestlingTournamentSystem.BusinessLogic.Validation;
using WrestlingTournamentSystem.DataAccess.DTO.User;
using WrestlingTournamentSystem.DataAccess.Entities;
using WrestlingTournamentSystem.DataAccess.Helpers.Exceptions;
using WrestlingTournamentSystem.DataAccess.Interfaces;

namespace WrestlingTournamentSystem.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly JwtTokenService _jwtTokenService;
        private readonly IAccountRepository _accountRepository;
        private readonly IValidationService _validationService;
        private readonly IMapper _mapper;

        public AccountService(JwtTokenService jwtTokenService, IValidationService validationService, IMapper mapper, IAccountRepository accountRepository)
        {
            _jwtTokenService = jwtTokenService;
            _validationService = validationService;
            _mapper = mapper;
            _accountRepository = accountRepository;
        }

        public async Task Register(RegisterUserDTO registerUserDTO)
        {
            var user = await _accountRepository.FindByUsernameAsync(registerUserDTO.UserName);

            if (user != null)
                throw new BusinessRuleValidationException($"Username {registerUserDTO.UserName} already taken");

            _validationService.ValidateRegisterPassword(registerUserDTO.Password, registerUserDTO.ConfirmPassword);

            var newUser = _mapper.Map<User>(registerUserDTO);

            await _accountRepository.CreateUserAsync(newUser, registerUserDTO.Password);
        }

        public async Task<SuccessfulLoginDTO> Login(LoginUserDTO loginUserDTO)
        {
            var user = await _accountRepository.FindByUsernameAsync(loginUserDTO.UserName);

            if (user == null)
                throw new BusinessRuleValidationException("Invalid username or password");

            var isPasswordValid = await _accountRepository.IsPasswordValidAsync(user, loginUserDTO.Password);

            if (!isPasswordValid)
                throw new BusinessRuleValidationException("Invalid username or password");

            var userRoles = await _accountRepository.GetUserRolesAsync(user);

            var accessToken = _jwtTokenService.CreateAccessToken(user.UserName!, user.Id, userRoles);

            return new SuccessfulLoginDTO(user.Id, accessToken);
        }

        public async Task<string> CreateRefreshToken(string userId)
        {
            var user = await _accountRepository.FindByIdAsync(userId);

            if (user == null)
                throw new NotFoundException("User was not found");

            return _jwtTokenService.CreateRefreshToken(user.Id);
        }

        public async Task<SuccessfulLoginDTO> GetAccessTokenFromRefreshToken(string? refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken) || 
                !_jwtTokenService.TryParseRefreshToken(refreshToken, out var claims) ||
                claims?.FindFirstValue(JwtRegisteredClaimNames.Sub) is not string userId)
            {
                throw new BusinessRuleValidationException("Invalid refresh token");
            }

            var user = await _accountRepository.FindByIdAsync(userId);

            if(user == null)  
                throw new BusinessRuleValidationException("Invalid refresh token");

            var userRoles = await _accountRepository.GetUserRolesAsync(user);

            var accessToken = _jwtTokenService.CreateAccessToken(user.UserName!, user.Id, userRoles);

            return new SuccessfulLoginDTO(user.Id, accessToken);
        }
    }
}
