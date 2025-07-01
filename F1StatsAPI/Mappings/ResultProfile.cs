using AutoMapper;
using F1StatsAPI.Models;
using F1StatsAPI.DTOs;

namespace F1StatsAPI.Mappings
{
    public class ResultProfile : Profile
    {
        public ResultProfile()
        {
            // Mapping fields from related GrandPrix entity (null-safe)
            CreateMap<Result, ResultDTO>()
                .ForMember(dest => dest.Number,
                    opt => opt.MapFrom(src => src.Driver != null ? src.Driver.Number : (int?)null))
 
                .ForMember(dest => dest.DriverName,
                    opt => opt.MapFrom(src => src.Driver != null ? src.Driver.GivenName + " " + src.Driver.FamilyName : null)) //Driver FULL NAME

                .ForMember(dest => dest.TeamName,
                    opt => opt.MapFrom(src => src.Team != null ? src.Team.Name : null)) //Team NAME

                .ForMember(dest => dest.RaceStatus,
                    opt => opt.MapFrom<RaceStatusResolver>()); //Race STATUS
        }
    }
}
