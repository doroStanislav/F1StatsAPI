namespace F1StatsAPI.DTOs
{
    public class DriverDTO
    {
        public int Number { get; set; }
        public string Code { get; set; } = string.Empty;
        public string GivenName { get; set; } = string.Empty;
        public string FamilyName { get; set; } = string.Empty;
        public string BirthDate { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string? TeamName { get; set; }
    }
}
