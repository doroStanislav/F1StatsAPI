using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using F1StatsAPI.Validators;

namespace F1StatsAPI.Models
{
    public class GrandPrix
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Name must be at least 2 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Country must be at least 2 characters.")]
        public string CircuitName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [Range(40, 80, ErrorMessage = "Laps must be between 40 and 80.")]
        public int Laps { get; set; }

        [Required]
        [Range(250, 315, ErrorMessage = "Distance must be between 250 and 315 km.")]
        public double Distance { get; set; }
    }
}
