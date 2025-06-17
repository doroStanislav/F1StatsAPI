using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using F1StatsAPI.Validators;

namespace F1StatsAPI.Models
{
    public class Driver
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Code must be exactly 3 characters.")]
        [RegularExpression("^[A-Z]{3}$", ErrorMessage = "Code must contain only uppercase letters (A-Z).")]
        public string Code { get; set; } = string.Empty;

        [Required]
        [Range(1, 99)]
        [CustomValidation(typeof(NumberValidation), nameof(NumberValidation.ValidateDriverNumber))]
        public int Number { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Must be at least 2 characters.")]
        [RegularExpression(@"^[A-Z][a-z]+$", ErrorMessage = "Must start with a capital letter and contain only letters.")]
        public string GivenName { get; set; } = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Must be at least 2 characters.")]
        [RegularExpression(@"^[A-Z][a-z]+$", ErrorMessage = "Must start with a capital letter and contain only letters.")]
        public string FamilyName { get; set; } = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Must be at least 2 characters.")]
        [RegularExpression(@"^[A-Z][a-z]+(?: [A-Z][a-z]+)*$", ErrorMessage = "Must be capitalized and contain only letters (e.g., 'United Kingdom').")]
        public string Country { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(DateValidator), nameof(DateValidator.ValidationBirthDate))]
        public DateTime DateOfBirth { get; set; }

        [ForeignKey("Team")]
        [Required]
        public int TeamId { get; set; }
        public required Team Team { get; set; }
    }
}
