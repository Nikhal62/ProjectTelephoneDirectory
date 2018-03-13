namespace Common.CommonDTO
{
    public class TelephoneDirectoryRecordDTO
    {
        public TelephoneDirectoryRecordDTO()
        {

        }

        public TelephoneDirectoryRecordDTO(string firstName, string lastName, string address, string phoneType, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            PhoneType = phoneType;
            PhoneNumber = phoneNumber;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string PhoneType { get; set; }

        public string PhoneNumber { get; set; }
    }
}
