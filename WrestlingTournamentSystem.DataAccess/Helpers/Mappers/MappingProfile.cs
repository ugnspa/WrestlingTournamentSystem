﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
            CreateMap<Tournament, TournamentReadDto>();
            CreateMap<TournamentCreateDto, Tournament>();
            CreateMap<TournamentUpdateDto, Tournament>();

            //TournamentWeightCategory
            CreateMap<TournamentWeightCategory, TournamentWeightCategoryReadDto>();
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
            CreateMap<IdentityRole,RoleDto>();
        }
    }
}
