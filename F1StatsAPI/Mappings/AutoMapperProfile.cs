using AutoMapper;
using F1StatsAPI.DTOs;
using F1StatsAPI.Models;

namespace F1StatsAPI.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Driver, DriverDTO>()
                .ForMember(dest => dest.TeamName,
                 opt => opt.MapFrom(src => src.Team != null? src.Team.Name : null));

            CreateMap<GrandPrix, GrandPrixDTO>();

            CreateMap<Team, TeamDTO>()
                .ForMember(dest => dest.DriverNames,
                    opt => opt.MapFrom(src => 
                        src.Drivers.Select(d => d.GivenName + " " + d.FamilyName).ToList()
                    ));
        }
    }
}
