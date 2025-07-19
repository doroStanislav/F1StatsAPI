namespace F1StatsAPI.DTOs
{
    public class TeamStandingDTO
    {
        public int Position { get; set; }
        public string TeamName { get; set; } = string.Empty;
        public int Points { get; set; }
    }
}
