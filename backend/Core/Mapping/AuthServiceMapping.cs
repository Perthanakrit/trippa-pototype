using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Interface.Services;
using Domain.Entities;

namespace Core.Mapping
{
    public class AuthServiceMapping : Profile
    {
        public AuthServiceMapping()
        {
            CreateMap<RegisterServiceInput, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.DisplayName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Contacts, opt => opt.MapFrom(src => src.Contacts));
        }
    }
}