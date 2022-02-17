using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Kerekes_Denisa_SADE.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int ChocolateID { get; set; }
        public DateTime OrderDate { get; set; }
        public Customer Customer { get; set; }
        public Chocolate Chocolate { get; set; }
    }
}
