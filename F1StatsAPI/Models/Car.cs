using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace F1StatsAPI.Models
{
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Must be at least 2 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string ChassisCode { get; set; } = string.Empty;

        [Required]
        public string PoweUnit { get; set; } = string.Empty;
    }
}
