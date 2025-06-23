namespace F1StatsAPI.DTOs
{
    public class GrandPrixDTO
    {
        public string Name { get; set; } = string.Empty;
        public string CircuitName { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int Laps { get; set; }
        public double Distance { get; set; }
    }
}
