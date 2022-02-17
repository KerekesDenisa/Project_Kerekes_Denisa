using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Kerekes_Denisa_SADE.Models
{
    public class Chocolate
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Flavour { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        public decimal Weight { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<SuppliedChocolate> SuppliedChocolates { get; set; }
    }
}
