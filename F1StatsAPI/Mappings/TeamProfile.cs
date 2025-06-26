using AutoMapper;
using F1StatsAPI.Models;
using F1StatsAPI.DTOs;

namespace F1StatsAPI.Mappings
{
    public class TeamProfile : Profile
    {
        public TeamProfile() 
        {
            CreateMap<Team, TeamDTO>()
                .ForMember(dest => dest.DriverNames,
                    opt => opt.MapFrom(src =>
                        src.Drivers.Select(d => d.GivenName + " " + d.FamilyName).ToList()
                    ));
        }
    }
}
