using Common.CommonDTO;
using System.Collections.Generic;

namespace Business_Logic_Layer.Service
{
    public interface ITelephoneDirectoryService
    {
        IEnumerable<TelephoneDirectoryRecordDTO> ViewSearchedRecords(string firstNameSearch, string lastNameSearch, string addressSearch, string phoneTypeSearch, string phoneNumberSearch);

        //method to check whether the phone number is duplicate entry
        bool CheckDuplicate(string phoneNumber,string phoneType);

        void CreateRecord(TelephoneDirectoryRecordDTO objTelephoneDto);

        TelephoneDirectoryRecordDTO FindRecordDetails(string phoneNumber);

        void UpdateRecord(TelephoneDirectoryRecordDTO record,string phoneNumberOld);

        void DeleteRecord(string phoneNumber);
    }
}
