using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project_Kerekes_Denisa_SADE.Models;

namespace Project_Kerekes_Denisa_SADE.Data
{
    public class DbInitializer
    {
        public static void Initialize(ShopContext context)
        {
            context.Database.EnsureCreated();
            if (context.Chocolates.Any())
            {
                return; // BD a fost creata anterior
            }
            var chocolates = new Chocolate[]
            {
                 new Chocolate{Name="Milka",Flavour="Luxuriously creamy with strawberry",Weight=Decimal.Parse("100"),Price=Decimal.Parse("10")},
                 new Chocolate{Name="Heidi",Flavour="Tenderness and creamy Milky Filling",Weight=Decimal.Parse("150"),Price=Decimal.Parse("15")},
                 new Chocolate{Name="Schogetten",Flavour="Sweet German Chocolate",Weight=Decimal.Parse("100"),Price=Decimal.Parse("5")},
                 new Chocolate{Name="Kinder",Flavour="A Fine Milk Chocolate with a Milky Filling",Weight=Decimal.Parse("100"),Price=Decimal.Parse("10")},
                 new Chocolate{Name="KitKat",Flavour="Amplified—bitter, lightly sweet, crunchy",Weight=Decimal.Parse("75"),Price=Decimal.Parse("8")},
                 new Chocolate{Name="Mars",Flavour="Delicious nougat and caramel covered in rich milk chocolate",Weight=Decimal.Parse("50"),Price=Decimal.Parse("15")},
                 new Chocolate{Name="Lion",Flavour="Chewy caramel, crispy wafer and crunchy cereals covered in chocolate",Weight=Decimal.Parse("50"),Price=Decimal.Parse("7")},
                 new Chocolate{Name="Bounty",Flavour="Luxurious coconut-filled, chocolate-enrobed candy",Weight=Decimal.Parse("150"),Price=Decimal.Parse("70")},
                 new Chocolate{Name="Ferrero Rocher",Flavour="A Whole Roasted Hazelnut encased in a thin wafer shell filled with Hazelnut Cream",Weight=Decimal.Parse("180"),Price=Decimal.Parse("20")},
             };
            foreach (Chocolate c in chocolates)
            {
                context.Chocolates.Add(c);
            }
            context.SaveChanges();
            var customers = new Customer[]
            {

                 new Customer{CustomerID=10,Name="David Hanz",BirthDate=DateTime.Parse("1996-08-12")},
                 new Customer{CustomerID=20,Name="Andrey Smith",BirthDate=DateTime.Parse("1995-10-20")},
                 new Customer{CustomerID=30,Name="Tudor Popescu",BirthDate=DateTime.Parse("1982-08-13")},
                 new Customer{CustomerID=40,Name="Tom Cruise",BirthDate=DateTime.Parse("1962-07-03")},

            };
            foreach (Customer r in customers)
            {
                context.Customers.Add(r);
            }
            context.SaveChanges();
            var orders = new Order[]
            {
                 new Order{ChocolateID=1,CustomerID=10},
                 new Order{ChocolateID=3,CustomerID=10},
                 new Order{ChocolateID=2,CustomerID=20},
                 new Order{ChocolateID=4,CustomerID=30},
                 new Order{ChocolateID=5,CustomerID=40},
                 new Order{ChocolateID=6,CustomerID=20},
                 new Order{ChocolateID=1,CustomerID=30},
                 new Order{ChocolateID=2,CustomerID=40},
            };
            foreach (Order e in orders)
            {
                context.Orders.Add(e);
            }
            context.SaveChanges();
            var suppliers = new Supplier[]
 {

             new Supplier{SupplierName="Lidl",Adress="Strada Alexandru Vaida Voevod 59, Cluj-Napoca"},
             new Supplier{SupplierName="Auchan",Adress="Strada Alexandru Vaida Voevod 60, Cluj-Napoca"},
             new Supplier{SupplierName="Kaufland 45",Adress="Strada Alexandru Vaida Voevod 53b, Cluj-Napoca"},
             };
            foreach (Supplier p in suppliers)
            {
                context.Suppliers.Add(p);
            }
            context.SaveChanges();
            var suppliedchocolates = new SuppliedChocolate[]
            {
                     new SuppliedChocolate {
                     ChocolateID = chocolates.Single(c => c.Name == "Milka" ).ID,
                     SupplierID = suppliers.Single(i => i.SupplierName == "Lidl").ID
                     },
                     new SuppliedChocolate {
                     ChocolateID = chocolates.Single(c => c.Name == "Mars" ).ID,
                    SupplierID = suppliers.Single(i => i.SupplierName == "Auchan").ID
                     },
                     new SuppliedChocolate {
                     ChocolateID = chocolates.Single(c => c.Name == "KitKat").ID,
                     SupplierID = suppliers.Single(i => i.SupplierName == "Kaufland").ID
                     },
                     new SuppliedChocolate {
                     ChocolateID = chocolates.Single(c => c.Name == "Bounty" ).ID,
                    SupplierID = suppliers.Single(i => i.SupplierName == "Lidl").ID
                     },
                     new SuppliedChocolate {
                     ChocolateID = chocolates.Single(c => c.Name == "Ferrero Rocher" ).ID,
                    SupplierID = suppliers.Single(i => i.SupplierName == "Kaufland").ID
                     },
                     new SuppliedChocolate {
                     ChocolateID = chocolates.Single(c => c.Name == "Lion" ).ID,
                     SupplierID = suppliers.Single(i => i.SupplierName == "Auchan").ID
                     },
                     new SuppliedChocolate {
                     ChocolateID = chocolates.Single(c => c.Name == "Kinder" ).ID,
                     SupplierID = suppliers.Single(i => i.SupplierName == "Lidl").ID
                     },
                      new SuppliedChocolate {
                     ChocolateID = chocolates.Single(c => c.Name == "Schogetten" ).ID,
                     SupplierID = suppliers.Single(i => i.SupplierName == "Auchan").ID
                     },
                     new SuppliedChocolate {
                     ChocolateID = chocolates.Single(c => c.Name == "Bounty" ).ID,
                     SupplierID = suppliers.Single(i => i.SupplierName == "Kaufland").ID
                     },
                       new SuppliedChocolate {
                     ChocolateID = chocolates.Single(c => c.Name == "Heidi" ).ID,
                     SupplierID = suppliers.Single(i => i.SupplierName == "Auchan").ID
 },
            };
            foreach (SuppliedChocolate pb in suppliedchocolates)
            {
                context.SuppliedChocolates.Add(pb);
            }
            context.SaveChanges();
        }


    }
}
