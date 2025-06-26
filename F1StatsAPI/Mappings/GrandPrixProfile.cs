using AutoMapper;
using F1StatsAPI.Models;
using F1StatsAPI.DTOs;

namespace F1StatsAPI.Mappings
{
    public class GrandPrixProfile : Profile
    {
        public GrandPrixProfile() 
        {
            CreateMap<GrandPrix, GrandPrixDTO>();
        }
    }
}
