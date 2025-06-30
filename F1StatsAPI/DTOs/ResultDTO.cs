namespace F1StatsAPI.DTOs
{
    public class ResultDTO
    {
        public string GrandPrixName { get; set; } = string.Empty;
        public DateTime GrandPrixDate { get; set; }

        public string DriverName { get; set; } = string.Empty;
        public string TeamName { get; set; } = string.Empty;

        public int? Position { get; set; }
        public int Points { get; set; }
        public int? Laps { get; set; }

        public string? RaceStatus { get; set; }
    }
}
