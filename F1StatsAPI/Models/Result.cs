using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace F1StatsAPI.Models
{
    public class Result
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int GrandPrixId { get; set; }
        public required GrandPrix GrandPrix { get; set; }
        public int DriverId { get; set; }
        public required Driver Driver { get; set; }

        public int TeamId { get; set; }
        public required Team Team { get; set; }

        public int CarId { get; set; }
        public required Car Car { get; set; }

        [Required]
        [Range(1, 20, ErrorMessage = "Position must be between 1 and 20.")]
        public int Position { get; set; }

        [Required]
        [Range(0, 25, ErrorMessage = "Points must be between 0 and 25.")]
        public int Points { get; set; }

        public TimeSpan? Time { get; set; }
        public string? GapToLeader { get; set; }

        [Required]
        public bool DidNotFinish { get; set; }
    }
}
