namespace F1StatsAPI.DTOs
{
    public class ResultDTO
    {
        public int? Position { get; set; }

        public int Number { get; set; }
        public string DriverName { get; set; } = string.Empty;
        public string TeamName { get; set; } = string.Empty;

        public int? Laps { get; set; }
        public string? RaceStatus { get; set; }
        public int Points { get; set; }
    }
}
