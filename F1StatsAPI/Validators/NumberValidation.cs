using System.ComponentModel.DataAnnotations;

namespace F1StatsAPI.Validators
{
    public class NumberValidation
    {
        public static ValidationResult? ValidateDriverNumber(int number, ValidationContext context)
        {
            if (number == 17)
            {
                return new ValidationResult("Number 17 is retired and cannot be used.");
            }

            return ValidationResult.Success;
        }
    }
}
