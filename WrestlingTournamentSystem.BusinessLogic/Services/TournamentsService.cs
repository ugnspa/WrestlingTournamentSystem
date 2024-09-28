using System;
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

namespace WrestlingTournamentSystem.BusinessLogic.Services
{
    public class TournamentsService : ITournamentsService
    {
        private readonly ITournamentRepository _tournamentsRepository;
        private readonly ITournamentStatusRepository _tournamentStatusRepository;
        private readonly IMapper _mapper;
        public TournamentsService(ITournamentRepository tournamentsRepository, ITournamentStatusRepository tournamentStatusRepository, IMapper mapper) 
        {
            _tournamentsRepository = tournamentsRepository;
            _tournamentStatusRepository = tournamentStatusRepository;
            _mapper = mapper;
        }
        public async Task<TournamentReadDTO> CreateTournamentAsync(TournamentCreateDTO tournamentCreateDTO)
        {
            if (tournamentCreateDTO == null)
                throw new ArgumentNullException(nameof(tournamentCreateDTO));

            var tournament = _mapper.Map<Tournament>(tournamentCreateDTO);
            var closedStatus = await _tournamentStatusRepository.GetClosedTournamentStatus();

            if (closedStatus == null)
                throw new NotFoundException("Closed status was not found");

            tournament.TournamentStatus = closedStatus;

            var result = await _tournamentsRepository.CreateTournamentAsync(tournament);

            if(result == null)
                throw new Exception("Failed to create tournament");

            return _mapper.Map<TournamentReadDTO>(result);
        }

        public async Task DeleteTournamentAsync(int id)
        {
            var tournament = await _tournamentsRepository.GetTournamentAsync(id);

            if (tournament == null)
                throw new NotFoundException($"Tournament with id {id} was not found");

            await _tournamentsRepository.DeleteTournamentAsync(tournament);
        }

        public async Task<TournamentReadDTO> GetTournamentAsync(int id)
        {
            var tournament = await _tournamentsRepository.GetTournamentAsync(id);

            if (tournament == null)
                throw new NotFoundException($"Tournament with id {id} was not found");

            return _mapper.Map<TournamentReadDTO>(tournament);
        }

        public async Task<IEnumerable<TournamentReadDTO>> GetTournamentsAsync()
        {
            var tournaments = await _tournamentsRepository.GetTournamentsAsync();

            return _mapper.Map<IEnumerable<TournamentReadDTO>>(tournaments);
        }

        public async Task<TournamentReadDTO> UpdateTournamentAsync(int tournamentId, TournamentUpdateDTO tournamentUpdateDTO)
        {
            if (tournamentUpdateDTO == null)
                throw new ArgumentNullException(nameof(tournamentUpdateDTO));

            var tournamentStatusExists = await _tournamentStatusRepository.TournamentStatusExists(tournamentUpdateDTO.StatusId);

            if (!tournamentStatusExists)
                throw new NotFoundException($"Tournament status with id {tournamentUpdateDTO.StatusId} was not found");

            var tournamentToUpdate = await _tournamentsRepository.GetTournamentAsync(tournamentId);

            if (tournamentToUpdate == null)
                throw new NotFoundException($"Tournament with id {tournamentId} was not found");

            _mapper.Map(tournamentUpdateDTO, tournamentToUpdate);   

            var result = await _tournamentsRepository.UpdateTournamentAsync(tournamentToUpdate);

            if (result == null)
                throw new Exception("Failed to update tournament");

            return _mapper.Map<TournamentReadDTO>(result);
        }
    }
}
