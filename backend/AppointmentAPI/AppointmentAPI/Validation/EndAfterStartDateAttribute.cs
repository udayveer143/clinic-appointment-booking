using System.ComponentModel.DataAnnotations;

namespace AppointmentAPI.Validation
{
    public class EndAfterStartDateAttribute : ValidationAttribute
    {
        private readonly string _startDateProperty;

        public EndAfterStartDateAttribute(string startDateProperty)
        {
            _startDateProperty = startDateProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var startProp = validationContext.ObjectType.GetProperty(_startDateProperty);
            if (startProp == null)
                return new ValidationResult($"Unknown property {_startDateProperty}");

            var startDateTime = ((DateTime)startProp.GetValue(validationContext.ObjectInstance)!).ToLocalTime();
            var endDateTime = ((DateTime)value!).ToLocalTime();

            if (endDateTime <= startDateTime)
                return new ValidationResult(ErrorMessage ?? "End time must be after start time.");

            return ValidationResult.Success!;
        }
    }
}
