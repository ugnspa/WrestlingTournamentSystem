﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WrestlingTournamentSystem.BusinessLogic.Interfaces;
using WrestlingTournamentSystem.DataAccess.Entities;
using WrestlingTournamentSystem.DataAccess.Interfaces;
using AutoMapper;
using WrestlingTournamentSystem.DataAccess.DTO.Tournament;
using WrestlingTournamentSystem.DataAccess.Exceptions;
using WrestlingTournamentSystem.BusinessLogic.Validation;

namespace WrestlingTournamentSystem.BusinessLogic.Services
{
    public class TournamentsService : ITournamentsService
    {
        private readonly ITournamentRepository _tournamentRepository;
        private readonly ITournamentStatusRepository _tournamentStatusRepository;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;
        public TournamentsService(ITournamentRepository tournamentRepository, ITournamentStatusRepository tournamentStatusRepository, IMapper mapper, IValidationService validationService) 
        {
            _tournamentRepository = tournamentRepository;
            _tournamentStatusRepository = tournamentStatusRepository;
            _mapper = mapper;
            _validationService = validationService;
        }
        public async Task<TournamentReadDTO> CreateTournamentAsync(TournamentCreateDTO tournamentCreateDTO)
        {
            if (tournamentCreateDTO == null)
                throw new ArgumentNullException(nameof(tournamentCreateDTO));

            _validationService.ValidateStartEndDates(tournamentCreateDTO.StartDate, tournamentCreateDTO.EndDate);

            var tournament = _mapper.Map<Tournament>(tournamentCreateDTO);
            var closedStatus = await _tournamentStatusRepository.GetClosedTournamentStatus();

            if (closedStatus == null)
                throw new NotFoundException("Closed status was not found");

            tournament.TournamentStatus = closedStatus;

            var result = await _tournamentRepository.CreateTournamentAsync(tournament);

            if(result == null)
                throw new Exception("Failed to create tournament");

            return _mapper.Map<TournamentReadDTO>(result);
        }

        public async Task DeleteTournamentAsync(int id)
        {
            var tournament = await _tournamentRepository.GetTournamentAsync(id);

            if (tournament == null)
                throw new NotFoundException($"Tournament with id {id} was not found");

            await _tournamentRepository.DeleteTournamentAsync(tournament);
        }

        public async Task<TournamentReadDTO> GetTournamentAsync(int id)
        {
            var tournament = await _tournamentRepository.GetTournamentAsync(id);

            if (tournament == null)
                throw new NotFoundException($"Tournament with id {id} was not found");

            return _mapper.Map<TournamentReadDTO>(tournament);
        }

        public async Task<IEnumerable<TournamentReadDTO>> GetTournamentsAsync()
        {
            var tournaments = await _tournamentRepository.GetTournamentsAsync();

            return _mapper.Map<IEnumerable<TournamentReadDTO>>(tournaments);
        }

        public async Task<TournamentReadDTO> UpdateTournamentAsync(int tournamentId, TournamentUpdateDTO tournamentUpdateDTO)
        {
            if (tournamentUpdateDTO == null)
                throw new ArgumentNullException(nameof(tournamentUpdateDTO));

            var tournamentStatusExists = await _tournamentStatusRepository.TournamentStatusExists(tournamentUpdateDTO.StatusId);

            if (!tournamentStatusExists)
                throw new NotFoundException($"Tournament status with id {tournamentUpdateDTO.StatusId} was not found");

            var tournamentToUpdate = await _tournamentRepository.GetTournamentAsync(tournamentId);

            if (tournamentToUpdate == null)
                throw new NotFoundException($"Tournament with id {tournamentId} was not found");

            _validationService.ValidateStartEndDates(tournamentUpdateDTO.StartDate, tournamentUpdateDTO.EndDate);

            _mapper.Map(tournamentUpdateDTO, tournamentToUpdate);   

            var result = await _tournamentRepository.UpdateTournamentAsync(tournamentToUpdate);

            if (result == null)
                throw new Exception("Failed to update tournament");

            return _mapper.Map<TournamentReadDTO>(result);
        }
    }
}
