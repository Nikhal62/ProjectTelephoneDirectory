using Business_Logic_Layer.Service;
using Common.CommonDTO;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Net;
using Presentation_Layer.Mapping;
using Presentation_Layer.ViewModel;
using Presentation_Layer.Paging;

namespace Presentation_Layer.Controllers
{
    public class TelephoneDirectoryController : Controller
    {
        //for using the phone number value in two action methods
        static string phoneNumberOld;

        //for getting the searched values in index post request
        static string _firstNameSearch;
        static string _lastNameSearch;
        static string _addressSearch;
        static string _phoneTypeSearch;
        static string _phoneNumberSearch;

        //for conversion of objects from one type to another
        Object_Conversion objConversion = new Object_Conversion();

        //for setting up pagination in display
        Pagination objPaging = new Pagination();

        private ITelephoneDirectoryService objTelephone;

        //injection through constructor  
        public TelephoneDirectoryController(ITelephoneDirectoryService tmpService)
        {
            objTelephone = tmpService;
        }

        public ActionResult Index(string firstNameSearch,string lastNameSearch, string addressSearch, string phoneTypeSearch,string phoneNumberSearch)
        {
            if ((phoneTypeSearch == "Select one") &&(firstNameSearch==null))
            {
                phoneTypeSearch = null;
            }       
            else if((phoneTypeSearch == "Select one") && (firstNameSearch == string.Empty))
            {
                phoneTypeSearch = string.Empty;
            }
            else if(phoneTypeSearch == "Select one")
            {
                phoneTypeSearch = string.Empty;
            }      
            IEnumerable<TelephoneDirectoryRecordDTO> telephonerecordlist;
            //calling method to search for phone number
            telephonerecordlist = objTelephone.ViewSearchedRecords(firstNameSearch,lastNameSearch,addressSearch,phoneTypeSearch,phoneNumberSearch);

            List<TelephoneDirectoryRecordModel> recordlist = new List<TelephoneDirectoryRecordModel>();

            foreach (var obj in telephonerecordlist)
            {
                recordlist.Add(new TelephoneDirectoryRecordModel(obj.FirstName, obj.LastName, obj.Address, obj.PhoneType, obj.PhoneNumber));
            }

            //assingning the searched values to the static variables for accessing from post index action
            _firstNameSearch = firstNameSearch;
            _lastNameSearch = lastNameSearch;
            _addressSearch = addressSearch;
            _phoneTypeSearch = phoneTypeSearch;
            _phoneNumberSearch = phoneNumberSearch;

            ViewData["phonetype"] = string.Empty ;
            //calling method to set pagination parameters
            return View(objPaging.GetTelephoneDirectoryViewModel(1,recordlist));
        }

        [HttpPost]
        public ActionResult Index(int currentPageIndex)
        {
            IEnumerable<TelephoneDirectoryRecordDTO> telephonerecordlist;
            //calling method to search for phone number
            telephonerecordlist = objTelephone.ViewSearchedRecords(_firstNameSearch, _lastNameSearch, _addressSearch, _phoneTypeSearch, _phoneNumberSearch);

            List<TelephoneDirectoryRecordModel> recordlist = new List<TelephoneDirectoryRecordModel>();

            foreach (var obj in telephonerecordlist)
            {
                recordlist.Add(new TelephoneDirectoryRecordModel(obj.FirstName, obj.LastName, obj.Address, obj.PhoneType, obj.PhoneNumber));
            }

            //setting searched values in viewdata for retrieving them in view
            if(!string.IsNullOrEmpty(_firstNameSearch))
                ViewData["firstname"] = _firstNameSearch;
            if (!string.IsNullOrEmpty(_lastNameSearch))
                ViewData["lastname"] = _lastNameSearch;
            if (!string.IsNullOrEmpty(_addressSearch))
                ViewData["address"] = _addressSearch;
            if(_phoneTypeSearch!=null)
                ViewData["phonetype"] = _phoneTypeSearch;
            if (_phoneTypeSearch == null)
                ViewData["phonetype"] = string.Empty;
            if (!string.IsNullOrEmpty(_phoneNumberSearch))
                ViewData["phonenumber"] = _phoneNumberSearch;

            //calling method to set pagination parameters
            return View(objPaging.GetTelephoneDirectoryViewModel(currentPageIndex, recordlist));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TelephoneDirectoryRecordModel objTelephoneViewModel)
        {
            if (ModelState.IsValid)
            {
                //mapping view model object to dto type
                TelephoneDirectoryRecordDTO objTelephoneDto = objConversion.ModelToDtoMapping(objTelephoneViewModel);
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
            TelephoneDirectoryRecordModel objTelephoneRecordViewModel = objConversion.DtoToModelMapping(record);
            if (record == null)
            {
                return HttpNotFound();
            }
            return View(objTelephoneRecordViewModel);
        }

        [HttpPost]
        public ActionResult Edit(TelephoneDirectoryRecordModel objTelephoneViewModel)
        {
            if (ModelState.IsValid)
            {
                TelephoneDirectoryRecordDTO objTelephoneDto = objConversion.ModelToDtoMapping(objTelephoneViewModel);
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
            TelephoneDirectoryRecordModel objTelephoneRecordViewModel = objConversion.DtoToModelMapping(record);
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
            TelephoneDirectoryRecordModel objTelephoneRecordViewModel = objConversion.DtoToModelMapping(record);

            if (record == null)
            {
                return HttpNotFound();
            }
            return View(objTelephoneRecordViewModel);
        }
    }
}