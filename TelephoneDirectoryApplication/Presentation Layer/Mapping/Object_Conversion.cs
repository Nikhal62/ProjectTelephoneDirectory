using Common.CommonDTO;
using Presentation_Layer.ViewModel;

namespace Presentation_Layer.Mapping
{
    public class Object_Conversion
    {
        //method which maps viewmodel object to dto object
        public TelephoneDirectoryRecordDTO ViewModelToDtoMapping(TelephoneDirectoryRecordViewModel objTelephoneViewModel)
        {
            TelephoneDirectoryRecordDTO objTelephoneDto = new TelephoneDirectoryRecordDTO
            {
                FirstName = objTelephoneViewModel.FirstName,
                LastName = objTelephoneViewModel.LastName,
                Address = objTelephoneViewModel.Address,
                PhoneType = objTelephoneViewModel.PhoneType,
                PhoneNumber = objTelephoneViewModel.PhoneNumber
            };
            return objTelephoneDto;
        }

        //method which maps dto object to viewmodel object
        public TelephoneDirectoryRecordViewModel DtoToViewModelMapping(TelephoneDirectoryRecordDTO objTelephoneDto)
        {
            TelephoneDirectoryRecordViewModel objTelephoneViewModel = new TelephoneDirectoryRecordViewModel
            {
                FirstName = objTelephoneDto.FirstName,
                LastName = objTelephoneDto.LastName,
                Address = objTelephoneDto.Address,
                PhoneType = objTelephoneDto.PhoneType,
                PhoneNumber = objTelephoneDto.PhoneNumber
            };
            return objTelephoneViewModel;
        }
    }
}