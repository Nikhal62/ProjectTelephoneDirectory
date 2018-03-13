using Presentation_Layer.CustomValidators;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Presentation_Layer.ViewModel
{
    public class TelephoneDirectoryRecordViewModel
    {
        public TelephoneDirectoryRecordViewModel()
        {

        }

        public TelephoneDirectoryRecordViewModel(string firstName, string lastName, string address, string phoneType, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            PhoneType = phoneType;
            PhoneNumber = phoneNumber;
        }

        [DataType(DataType.Text)]
        [NameCustomValidator]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        [NameCustomValidator]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Address is mandatory.")]
        public string Address { get; set; }

        [DisplayName("Phone Type")]
        [Required(ErrorMessage = "Select any one of the phone type.")]
        public string PhoneType { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Phone Number")]
        [Required(ErrorMessage = "Phone Number is mandatory.")]
        [PhoneNumberCustomValidator]
        public string PhoneNumber { get; set; }
    }
}