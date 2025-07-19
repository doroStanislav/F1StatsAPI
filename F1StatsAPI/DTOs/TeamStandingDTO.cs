using System.Text.Json.Serialization;

namespace F1StatsAPI.DTOs
{
    public class TeamStandingDTO
    {
        public int Position { get; set; }

        [JsonIgnore]
        public int TeamId { get; set; }
        public string TeamName { get; set; } = string.Empty;
        public int TotalPoints { get; set; }
    }
}
