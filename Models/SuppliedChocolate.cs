using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Kerekes_Denisa_SADE.Models
{
    public class SuppliedChocolate
    {
        public int SupplierID { get; set; }
        public int ChocolateID { get; set; }
        public Supplier Supplier { get; set; }
        public Chocolate Chocolate { get; set; }
    }
}
