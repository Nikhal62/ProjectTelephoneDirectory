using Presentation_Layer.CustomValidators;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Presentation_Layer.ViewModel
{
    public class TelephoneDirectoryRecordModel
    {
        public TelephoneDirectoryRecordModel()
        {

        }

        public TelephoneDirectoryRecordModel(string firstName, string lastName, string address, string phoneType, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            PhoneType = phoneType;
            PhoneNumber = phoneNumber;
        }

        ///<summary>
        /// Gets or sets First Name.
        ///</summary>
        [DataType(DataType.Text)]
        [NameCustomValidator]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        ///<summary>
        /// Gets or sets Last Name.
        ///</summary>
        [DataType(DataType.Text)]
        [NameCustomValidator]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        ///<summary>
        /// Gets or sets Address.
        ///</summary>
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Address is mandatory.")]
        public string Address { get; set; }

        ///<summary>
        /// Gets or sets Phone Type.
        ///</summary>
        [DisplayName("Phone Type")]
        [Required(ErrorMessage = "Select any one of the phone type.")]
        public string PhoneType { get; set; }

        ///<summary>
        /// Gets or sets Phone Number.
        ///</summary>
        [DataType(DataType.Text)]
        [DisplayName("Phone Number")]
        [Required(ErrorMessage = "Phone Number is mandatory.")]
        [PhoneNumberCustomValidator]
        public string PhoneNumber { get; set; }
    }
}