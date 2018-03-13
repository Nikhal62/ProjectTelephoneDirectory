using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Presentation_Layer.CustomValidators
{
    public class NameCustomValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string name = value.ToString();

                if (name.Length>50)
                {
                    return new ValidationResult("" + validationContext.DisplayName + " should consists a maximum of 50 characters.");
                }
                else if(name.Any(char.IsDigit))
                {
                    return new ValidationResult("" + validationContext.DisplayName + " should not contain any digits");
                }
                else if(name.Contains(" "))
                {
                    return new ValidationResult("" + validationContext.DisplayName + " should not contain any space");
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            else
            {
                return new ValidationResult("" + validationContext.DisplayName + " is mandatory");
            }
        }
    }
}