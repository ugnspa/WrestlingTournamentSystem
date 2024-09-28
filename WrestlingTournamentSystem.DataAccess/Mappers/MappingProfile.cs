using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using WrestlingTournamentSystem.DataAccess.DTO.Tournament;
using WrestlingTournamentSystem.DataAccess.DTO.TournamentWeightCategory;
using WrestlingTournamentSystem.DataAccess.DTO.Wrestler;
using WrestlingTournamentSystem.DataAccess.Entities;

namespace WrestlingTournamentSystem.DataAccess.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Tournament
            CreateMap<Tournament, TournamentReadDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.TournamentStatus.Name));
            CreateMap<TournamentCreateDTO, Tournament>();
            CreateMap<TournamentUpdateDTO, Tournament>();

            //TournamentWeightCategory
            CreateMap<TournamentWeightCategory, TournamentWeightCategoryReadDTO>()
                .ForMember(dest => dest.Style, opt => opt.MapFrom(src => src.WeightCategory.WrestlingStyle.Name))
                .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.WeightCategory.Weight))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.WeightCategory.Age))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.TournamentWeightCategoryStatus.Name));
            CreateMap<TournamentWeightCategoryCreateDTO, TournamentWeightCategory>();
            CreateMap<TournamentWeightCategoryUpdateDTO, TournamentWeightCategory>();

            //Wrestler
            CreateMap<Wrestler, WrestlerReadDTO>()
                .ForMember(dest => dest.Style, opt => opt.MapFrom(src => src.WrestlingStyle.Name));
            CreateMap<WrestlerCreateDTO, Wrestler>();

        }
    }
}
