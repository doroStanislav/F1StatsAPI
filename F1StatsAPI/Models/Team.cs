using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace F1StatsAPI.Models
{
    public class Team
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Team name must be between 2 and 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50, ErrorMessage = "Team Chief name is too long.")]
        public string TeamChief { get; set; } = string.Empty;

        [Range(0, 100, ErrorMessage = "World Championships must be between 0 and 100.")]
        public int WorldChampionships { get; set; }

        [StringLength(100, ErrorMessage = "Base location is too long.")]
        public string BaseLocation { get; set; } = string.Empty;

        [Range(1900, 2100, ErrorMessage = "Foundation year must be between 1900 and 2100.")]
        public int FoundationYear { get; set; } 
    }
}
