using Data_Access_Layer.DBContext;
using Data_Access_Layer.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Data_Access_Layer.Repository
{
    public class TelephoneDirectoryRepository : ITelephoneDirectoryRepository
    {
        private TelephoneDirectoryDbContext context;

        //injection through constructor
        public TelephoneDirectoryRepository(TelephoneDirectoryDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<TelephoneDirectoryRecord> ViewSearchedRecords(string search)
        {
            if(search!=null)
                return context.Directory.Where(x => x.PhoneNumber.Contains(search)).ToList();
            else
                return context.Directory.ToList();
        }

        public bool CheckDuplicate(string phoneNumber,string phoneType)
        {
            if(phoneType.Equals("Landline"))
            {
                return context.Directory.Any(m => m.PhoneNumber == phoneNumber);
            }
            else if(phoneType.Equals("Mobile"))
            {
                var mobileList=context.Directory.Select(m => m.PhoneNumber).ToList();
                foreach(var strMobile in mobileList)
                {
                    string mobileNumber= strMobile.Substring(strMobile.Length-10,10);
                    if (mobileNumber == phoneNumber.Substring(phoneNumber.Length - 10, 10))
                        return true;
                }
            }
            return false;          
        }

        public void CreateRecord(TelephoneDirectoryRecord objTelephone)
        {
            context.Directory.Add(objTelephone);
            context.SaveChanges();
        }

        public TelephoneDirectoryRecord FindRecordDetails(string phoneNumber)
        {
            int id = FindId(phoneNumber);
            TelephoneDirectoryRecord record = context.Directory.Find(id);
            return record;
        }

        public void UpdateRecord(TelephoneDirectoryRecord record,string phoneNumberOld)
        {
            int id = FindId(phoneNumberOld);
            record.Id = id;
            context.Entry(record).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteRecord(string phoneNumber)
        {
            int id = FindId(phoneNumber);
            var objTelephone=context.Directory.Find(id);
            context.Directory.Remove(objTelephone);
            context.SaveChanges();
        }

        //method to find id of the specified phone number's record
        int FindId(string phoneNumber)
        {
            int id = context.Directory.Where(m => m.PhoneNumber == phoneNumber).Select(m => m.Id).FirstOrDefault();
            return id;
        }
    }
}
