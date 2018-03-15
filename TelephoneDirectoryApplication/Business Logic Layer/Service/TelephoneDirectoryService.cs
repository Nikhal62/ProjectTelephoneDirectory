using Common.CommonDTO;
using Data_Access_Layer.Model;
using Data_Access_Layer.Repository;
using System.Collections.Generic;

namespace Business_Logic_Layer.Service
{
    public class TelephoneDirectoryService : ITelephoneDirectoryService
    {
        private ITelephoneDirectoryRepository objTelephone;

        //injection through constructor  
        public TelephoneDirectoryService(ITelephoneDirectoryRepository tmpService)
        {
            objTelephone = tmpService;
        }

        public IEnumerable<TelephoneDirectoryRecordDTO> ViewSearchedRecords(string firstNameSearch, string lastNameSearch, string addressSearch, string phoneTypeSearch, string phoneNumberSearch)
        {
            IEnumerable<TelephoneDirectoryRecord> telephonerecordlist = objTelephone.ViewSearchedRecords(firstNameSearch,lastNameSearch,addressSearch,phoneTypeSearch,phoneNumberSearch);
            List<TelephoneDirectoryRecordDTO> recordlist = new List<TelephoneDirectoryRecordDTO>();
            foreach (var obj in telephonerecordlist)
            {
                recordlist.Add(new TelephoneDirectoryRecordDTO(obj.FirstName, obj.LastName, obj.Address, obj.PhoneType, obj.PhoneNumber));
            }
            return recordlist;
        }

        public bool CheckDuplicate(string phoneNumber,string phoneType)
        {
            return objTelephone.CheckDuplicate(phoneNumber,phoneType);
        }

        public void CreateRecord(TelephoneDirectoryRecordDTO objTelephoneDto)
        {
            TelephoneDirectoryRecord objTelephoneRecordModel = DtoToModelMapping(objTelephoneDto);
            objTelephone.CreateRecord(objTelephoneRecordModel);
        }

        public TelephoneDirectoryRecordDTO FindRecordDetails(string phoneNumber)
        {
            TelephoneDirectoryRecord objTelephoneRecordModel = objTelephone.FindRecordDetails(phoneNumber);
            TelephoneDirectoryRecordDTO objTelephoneRecordDto = ModelToDtoMapping(objTelephoneRecordModel);
            return objTelephoneRecordDto;
        }

        public void UpdateRecord(TelephoneDirectoryRecordDTO objTelephoneRecordDto, string phoneNumberOld)
        {
            TelephoneDirectoryRecord objTelephoneRecordModel = DtoToModelMapping(objTelephoneRecordDto);
            objTelephone.UpdateRecord(objTelephoneRecordModel, phoneNumberOld);
        }

        public void DeleteRecord(string phoneNumber)
        {
            objTelephone.DeleteRecord(phoneNumber);
        }

        //method which maps dto object to model object
        TelephoneDirectoryRecord DtoToModelMapping(TelephoneDirectoryRecordDTO objTelephoneDto)
        {
            TelephoneDirectoryRecord objTelephoneRecordModel = new TelephoneDirectoryRecord
            {
                FirstName = objTelephoneDto.FirstName,
                LastName = objTelephoneDto.LastName,
                Address = objTelephoneDto.Address,
                PhoneType = objTelephoneDto.PhoneType,
                PhoneNumber = objTelephoneDto.PhoneNumber
            };
            return objTelephoneRecordModel;
        }

        //method which maps model object to dto object
        TelephoneDirectoryRecordDTO ModelToDtoMapping(TelephoneDirectoryRecord objTelephoneRecordModel)
        {
            TelephoneDirectoryRecordDTO objTelephoneRecordDto = new TelephoneDirectoryRecordDTO
            {
                FirstName = objTelephoneRecordModel.FirstName,
                LastName = objTelephoneRecordModel.LastName,
                Address = objTelephoneRecordModel.Address,
                PhoneType = objTelephoneRecordModel.PhoneType,
                PhoneNumber = objTelephoneRecordModel.PhoneNumber
            };
            return objTelephoneRecordDto;
        }
    }
}
