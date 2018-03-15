using Presentation_Layer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Presentation_Layer.Paging
{
    public class Pagination
    {
        public TelephoneDirectoryRecordViewModel GetTelephoneDirectoryViewModel(int currentPage, List<TelephoneDirectoryRecordModel> recordlist)
        {
            TelephoneDirectoryRecordViewModel objPage = new TelephoneDirectoryRecordViewModel();

            //setting maximum number of records in a page
            int maxRows = 2;

            //setting the records for display in the current page only
            objPage.Records = recordlist.Skip((currentPage - 1) * maxRows).Take(maxRows).ToList();

            //getting total page count based on number of records to display
            double pageCount = (double)((decimal)recordlist.Count() / Convert.ToDecimal(maxRows));

            //converting page count to gratest integer function
            objPage.PageCount = (int)Math.Ceiling(pageCount);

            objPage.CurrentPageIndex = currentPage;
            return objPage;
        }
    }
}