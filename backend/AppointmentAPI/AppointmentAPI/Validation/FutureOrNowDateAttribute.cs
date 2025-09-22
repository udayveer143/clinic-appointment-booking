using System.ComponentModel.DataAnnotations;

namespace AppointmentAPI.Validation
{
    public class FutureOrNowDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateTime)
            {
                // Convert to local time
                var localDateTime = dateTime.ToLocalTime();

                if (localDateTime < DateTime.Now)
                {
                    return new ValidationResult(ErrorMessage ?? "Start time must be current or in the future.");
                }
            }
            return ValidationResult.Success!;
        }
    }
}
