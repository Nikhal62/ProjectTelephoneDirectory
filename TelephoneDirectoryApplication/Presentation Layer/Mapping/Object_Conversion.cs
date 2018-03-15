using Common.CommonDTO;
using Presentation_Layer.ViewModel;

namespace Presentation_Layer.Mapping
{
    public class Object_Conversion
    {
        //method which maps viewmodel object to dto object
        public TelephoneDirectoryRecordDTO ModelToDtoMapping(TelephoneDirectoryRecordModel objTelephoneModel)
        {
            TelephoneDirectoryRecordDTO objTelephoneDto = new TelephoneDirectoryRecordDTO
            {
                FirstName = objTelephoneModel.FirstName,
                LastName = objTelephoneModel.LastName,
                Address = objTelephoneModel.Address,
                PhoneType = objTelephoneModel.PhoneType,
                PhoneNumber = objTelephoneModel.PhoneNumber
            };
            return objTelephoneDto;
        }

        //method which maps dto object to viewmodel object
        public TelephoneDirectoryRecordModel DtoToModelMapping(TelephoneDirectoryRecordDTO objTelephoneDto)
        {
            TelephoneDirectoryRecordModel objTelephoneModel = new TelephoneDirectoryRecordModel
            {
                FirstName = objTelephoneDto.FirstName,
                LastName = objTelephoneDto.LastName,
                Address = objTelephoneDto.Address,
                PhoneType = objTelephoneDto.PhoneType,
                PhoneNumber = objTelephoneDto.PhoneNumber
            };
            return objTelephoneModel;
        }
    }
}