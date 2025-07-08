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
        public string ChassisCode { get; set; } = string.Empty;

        [Required]
        public string PowerUnit { get; set; } = string.Empty;
    }
}
