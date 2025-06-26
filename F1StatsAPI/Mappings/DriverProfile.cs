using AutoMapper;
using System.Globalization;
using F1StatsAPI.DTOs;
using F1StatsAPI.Models;

namespace F1StatsAPI.Mappings
{
    public class DriverProfile : Profile
    {
        public DriverProfile()
        {
            CreateMap<Driver, DriverDTO>()
                .ForMember(dest => dest.TeamName,
                    opt => opt.MapFrom(src => src.Team != null ? src.Team.Name : null))
                .ForMember(dest => dest.BirthDate,
                    opt => opt.MapFrom(src => src.DateOfBirth.ToString("dd MMMM yyyy", CultureInfo.InvariantCulture)));
        }
    }
}
