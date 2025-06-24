using F1StatsAPI.Models;

namespace F1StatsAPI.DTOs
{
    public class TeamDTO
    {
        public string Name { get; set; } = string.Empty;
        public string TeamChief { get; set; } = string.Empty;
        public int WorldChampionships { get; set; }
        public int FoundationYear { get; set; }
        public string? CarChassisCode { get; set; }
        public List<string>? DriverNames { get; set; }
    }
}
