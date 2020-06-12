using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TakeHomeAPI.Models
{
    public class ContractModel
    {
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
        public System.DateTime DOB { get; set; }
        public Nullable<System.DateTime> SaleDate { get; set; }
        public string CoveragePlan { get; set; }
        public Nullable<int> NetPrice { get; set; }
        public int CustomerID { get; set; }

    }
}