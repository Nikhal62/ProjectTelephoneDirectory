using Business_Logic_Layer.Service;
using Common.CommonDTO;
using System.Collections.Generic;
using System.Web.Mvc;
using Presentation_Layer.ViewModel;
using System.Net;
using Presentation_Layer.Mapping;

namespace Presentation_Layer.Controllers
{
    public class TelephoneDirectoryController : Controller
    {
        //for using the phone number value in two action methods
        static string phoneNumberOld;

        //for conversion of objects from one type to another
        Object_Conversion objConversion = new Object_Conversion();

        private ITelephoneDirectoryService objTelephone;

        //injection through constructor  
        public TelephoneDirectoryController(ITelephoneDirectoryService tmpService)
        {
            objTelephone = tmpService;
        }

        public ActionResult Index(string search)
        {
            IEnumerable<TelephoneDirectoryRecordDTO> telephonerecordlist;
            //calling method to search for phone number
            telephonerecordlist = objTelephone.ViewSearchedRecords(search);         

            List<TelephoneDirectoryRecordViewModel> recordlist = new List<TelephoneDirectoryRecordViewModel>();
            foreach (var obj in telephonerecordlist)
            {
                recordlist.Add(new TelephoneDirectoryRecordViewModel(obj.FirstName, obj.LastName, obj.Address, obj.PhoneType, obj.PhoneNumber));
            }
            return View(recordlist);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TelephoneDirectoryRecordViewModel objTelephoneViewModel)
        {
            if (ModelState.IsValid)
            {
                //mapping view model object to dto type
                TelephoneDirectoryRecordDTO objTelephoneDto = objConversion.ViewModelToDtoMapping(objTelephoneViewModel);
                //check for duplicate entry of phone number to ensure uniqueness
                if (objTelephone.CheckDuplicate(objTelephoneDto.PhoneNumber,objTelephoneDto.PhoneType))
                {
                    ModelState.AddModelError("PhoneNumber", "This phone number already exists");
                    return View(objTelephoneViewModel);
                }
                //method to create new record
                objTelephone.CreateRecord(objTelephoneDto);
                return RedirectToAction("Index");
            }
            return View(objTelephoneViewModel);
        }

        public ActionResult Edit(string phoneNumber)
        {
            if (phoneNumber == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //storing the old phone number value before editing
            phoneNumberOld = phoneNumber;
               
            //Getting the details record of the specified phone number        
            TelephoneDirectoryRecordDTO record = objTelephone.FindRecordDetails(phoneNumber);

            //mapping dto object to view model type
            TelephoneDirectoryRecordViewModel objTelephoneRecordViewModel = objConversion.DtoToViewModelMapping(record);
            if (record == null)
            {
                return HttpNotFound();
            }
            return View(objTelephoneRecordViewModel);
        }

        [HttpPost]
        public ActionResult Edit(TelephoneDirectoryRecordViewModel objTelephoneViewModel)
        {
            if (ModelState.IsValid)
            {
                TelephoneDirectoryRecordDTO objTelephoneDto = objConversion.ViewModelToDtoMapping(objTelephoneViewModel);
                //Checking for duplication only if phone number is changed
                if (phoneNumberOld != objTelephoneDto.PhoneNumber)
                {
                    if (objTelephone.CheckDuplicate(objTelephoneDto.PhoneNumber, objTelephoneDto.PhoneType))
                    {
                        ModelState.AddModelError("PhoneNumber", "This phone number already exists");
                        return View(objTelephoneViewModel);
                    }
                }
                //method for updating the record
                objTelephone.UpdateRecord(objTelephoneDto,phoneNumberOld);
                return RedirectToAction("Index");
            }
            return View(objTelephoneViewModel);
        }

        public ActionResult Delete(string phoneNumber)
        {
            if (phoneNumber == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //getting the details record of the specified phone number 
            TelephoneDirectoryRecordDTO record = objTelephone.FindRecordDetails(phoneNumber);

            //mapping dto object to view model type
            TelephoneDirectoryRecordViewModel objTelephoneRecordViewModel = objConversion.DtoToViewModelMapping(record);
            if (record == null)
            {
                return HttpNotFound();
            }
            return View(objTelephoneRecordViewModel);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string phoneNumber)
        {
            //method to delete record
            objTelephone.DeleteRecord(phoneNumber);
            return RedirectToAction("Index");
        }

        public ActionResult Details(string phoneNumber)
        {
            if (phoneNumber == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //getting the details record of the specified phone number 
            TelephoneDirectoryRecordDTO record = objTelephone.FindRecordDetails(phoneNumber);
            
            //mapping dto object to view model type
            TelephoneDirectoryRecordViewModel objTelephoneRecordViewModel = objConversion.DtoToViewModelMapping(record);

            if (record == null)
            {
                return HttpNotFound();
            }
            return View(objTelephoneRecordViewModel);
        }
    }
}