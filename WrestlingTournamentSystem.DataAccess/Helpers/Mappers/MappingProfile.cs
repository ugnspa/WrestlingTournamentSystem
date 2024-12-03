using AutoMapper;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Routing.Constraints;
using WrestlingTournamentSystem.DataAccess.DTO.Tournament;
using WrestlingTournamentSystem.DataAccess.DTO.TournamentWeightCategory;
using WrestlingTournamentSystem.DataAccess.DTO.User;
using WrestlingTournamentSystem.DataAccess.DTO.Wrestler;
using WrestlingTournamentSystem.DataAccess.Entities;

namespace WrestlingTournamentSystem.DataAccess.Helpers.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Tournament
            CreateMap<Tournament, TournamentReadDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.TournamentStatus.Name));
            CreateMap<TournamentCreateDto, Tournament>();
            CreateMap<TournamentUpdateDto, Tournament>();

            //TournamentWeightCategory
            CreateMap<TournamentWeightCategory, TournamentWeightCategoryReadDto>()
                .ForMember(dest => dest.Style, opt => opt.MapFrom(src => src.WeightCategory.WrestlingStyle.Name))
                .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.WeightCategory.Weight))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.WeightCategory.Age))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.TournamentWeightCategoryStatus.Name));
            CreateMap<TournamentWeightCategoryCreateDto, TournamentWeightCategory>();
            CreateMap<TournamentWeightCategoryUpdateDto, TournamentWeightCategory>();

            //Wrestler
            CreateMap<Wrestler, WrestlerReadDto>()
                .ForMember(dest => dest.Style, opt => opt.MapFrom(src => src.WrestlingStyle.Name))
                .ForMember(dest => dest.Coach, opt => opt.MapFrom(src => src.Coach != null ? $"{src.Coach.Name} {src.Coach.Surname}" : null));
            CreateMap<WrestlerCreateDto, Wrestler>();
            CreateMap<WrestlerUpdateDto, Wrestler>();

            //User
            CreateMap<RegisterUserDto, User>();
            CreateMap<User, UserListDto>();
            CreateMap<User, UserDetailDto>();
        }
    }
}
