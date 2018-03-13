using Data_Access_Layer.Model;
using System.Collections.Generic;

namespace Data_Access_Layer.Repository
{
    public interface ITelephoneDirectoryRepository
    {
        IEnumerable<TelephoneDirectoryRecord> ViewSearchedRecords(string search);

        //method to check whether the phone number is duplicate entry
        bool CheckDuplicate(string phoneNumber,string phoneType);

        void CreateRecord(TelephoneDirectoryRecord objTelephone);

        TelephoneDirectoryRecord FindRecordDetails(string phoneNumber);

        void UpdateRecord(TelephoneDirectoryRecord record,string phoneNumberOld);

        void DeleteRecord(string phoneNumber);
    }
}
