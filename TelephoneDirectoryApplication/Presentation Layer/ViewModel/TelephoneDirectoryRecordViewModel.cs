using System;
using System.Collections.Generic;
using System.Linq;

namespace Presentation_Layer.ViewModel
{
    public class TelephoneDirectoryRecordViewModel
    {
        ///<summary>
        /// Gets or sets Records.
        ///</summary>
        public List<TelephoneDirectoryRecordModel> Records { get; set; }

        ///<summary>
        /// Gets or sets CurrentPageIndex.
        ///</summary>
        public int CurrentPageIndex { get; set; }

        ///<summary>
        /// Gets or sets PageCount.
        ///</summary>
        public int PageCount { get; set; }
    }
}