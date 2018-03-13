using System.ComponentModel.DataAnnotations;

namespace Data_Access_Layer.Model
{
    public class TelephoneDirectoryRecord
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string PhoneType { get; set; }

        public string PhoneNumber { get; set; }
    }
}
