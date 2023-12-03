using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Interface.Services;
using Domain.Entities;

namespace Core.Mapping
{
    public class CustomTripMapping : Profile
    {
        public CustomTripMapping()
        {
            CreateMap<CustomTripServiceInput, Trip>();
        }
    }
}