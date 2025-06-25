using AutoMapper;
using AutoMapper.Execution;
using F1StatsAPI.DTOs;
using F1StatsAPI.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace F1StatsAPI.Mappings
{
    public class RaceStatusResolver : IValueResolver<Result, ResultDTO, string?>
    {
        public string? Resolve (Result source, ResultDTO destination, string? destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Status))
                return source.Status;

            if (source.Position == 1 && source.Time != null)
                return source.Time.Value.ToString();

            if (!string.IsNullOrEmpty(source.GapToLeader))
                return source.GapToLeader;

            return "N/A";
        }
    }
}
