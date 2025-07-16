using System.Text.Json.Serialization;

namespace F1StatsAPI.DTOs
{
    public class DriverStandingDTO
    {
        public int Position { get; set; }

        [JsonIgnore]
        public int DriverId { get; set; }

        public string DriverName { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public string TeamName { get; set; } = string.Empty;
        public int TotalPoints { get; set; }
    }
}
