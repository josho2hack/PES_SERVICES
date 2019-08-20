using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point01Service
{
    public class RootObject
    {
        public TaxCollection[] TaxCol { get; set; }
    }

    public class TaxCollection
    {
        public decimal CMCYforcast { get; set; }
        public decimal CMcurrentYear { get; set; }

        public string officeCode { get; set; }
    }
}
