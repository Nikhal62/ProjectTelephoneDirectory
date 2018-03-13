using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Presentation_Layer.CustomValidators
{
    public class PhoneNumberCustomValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool flag = false;
            object instance = validationContext.ObjectInstance;
            Type type = instance.GetType();
            PropertyInfo property = type.GetProperty("PhoneType");
            object propertyValue = property.GetValue(instance);
            if(propertyValue==null||value==null)
                return ValidationResult.Success;

            if (propertyValue == null)
            {
                return new ValidationResult("Select any one of the phone type.");
            }
            else if(value==null)
            {
                return new ValidationResult("Phone Number is mandatory");
            }
            else
            {
                switch (propertyValue.ToString())
                {
                    case "Mobile":
                        if (Regex.IsMatch(value.ToString(), @"((\+91|0)[6-9]\d{9}$)", RegexOptions.IgnoreCase))
                        {
                            flag = true;
                        }
                        break;
                    case "Landline":
                        if (Regex.IsMatch(value.ToString(), @"((0\d{2}\d{8}$)|(0\d{3}\d{7}$)|(0\d{4}\d{6}$))", RegexOptions.IgnoreCase))
                        {
                            flag = true;
                        }
                        break;
                    default:
                        flag = true;
                        break;
                }
            }
            if (!flag)
            {
                if(propertyValue.ToString().Equals("Mobile"))
                {
                    return new ValidationResult("Mobile number format is not valid.Mobile number starts with either +91 or 0 followed by 4 digits operators code and 6 digits subscribers unique code.");
                }
                else if (propertyValue.ToString().Equals("Landline"))
                {
                    return new ValidationResult("Land phone number format is not valid.Land phone number starts with 0 followed by STD code and phone number.");
                }
            }
            else
            {
                return ValidationResult.Success;
            }
            return null;
        }
    }
} 