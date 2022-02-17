using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Kerekes_Denisa_SADE.Models.ShopViewModels
{
    public class SupplierIndexData
    {
        public IEnumerable<Supplier> Suppliers { get; set; }
        public IEnumerable<Chocolate> Chocolates { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
