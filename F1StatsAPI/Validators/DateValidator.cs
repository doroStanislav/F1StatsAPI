using System.ComponentModel.DataAnnotations;

namespace F1StatsAPI.Validators
{
    public class DateValidator
    {
        public static ValidationResult? ValidationDate(DateTime date, ValidationContext context)
        {
            if (date.Date > DateTime.Today)
            {
                return new ValidationResult("Date cannot be in the future.");
            }
            return ValidationResult.Success;
        }
    }
}
