using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_Kerekes_Denisa_SADE.Data;
using Project_Kerekes_Denisa_SADE.Models;
using Project_Kerekes_Denisa_SADE.Models.ShopViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Project_Kerekes_Denisa_SADE.Controllers
{
    [Authorize(Policy = "OnlySales")]
    public class SuppliersController : Controller
    {
        private readonly ShopContext _context;

        public SuppliersController(ShopContext context)
        {
            _context = context;
        }

        // GET: Suppliers
        public async Task<IActionResult> Index(int? id, int? chocolateID)
        {
            var viewModel = new SupplierIndexData();
            viewModel.Suppliers = await _context.Suppliers
            .Include(i => i.SuppliedChocolates)
            .ThenInclude(i => i.Chocolate)
            .ThenInclude(i => i.Orders)
            .ThenInclude(i => i.Customer)
            .AsNoTracking()
            .OrderBy(i => i.SupplierName)
            .ToListAsync();
            if (id != null)
            {
                ViewData["SupplierID"] = id.Value;
                Supplier supplier = viewModel.Suppliers.Where(
                i => i.ID == id.Value).Single();
                viewModel.Chocolates = supplier.SuppliedChocolates.Select(s => s.Chocolate);
            }
            if (chocolateID != null)
            {
                ViewData["ChocolateID"] = chocolateID.Value;
                viewModel.Orders = viewModel.Chocolates.Where(
                x => x.ID == chocolateID).Single().Orders;
            }
            return View(viewModel);
        }

        // GET: Suppliers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // GET: Suppliers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,SupplierName,Adress")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(supplier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        // GET: Suppliers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var supplier = await _context.Suppliers
            .Include(i => i.SuppliedChocolates).ThenInclude(i => i.Chocolate)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);
            if (supplier == null)
            {
                return NotFound();
            }
            PopulateSuppliedChocolateData(supplier);
            return View(supplier);

        }
        private void PopulateSuppliedChocolateData(Supplier supplier)
        {
            var allChocolates = _context.Chocolates;
            var supplierChocolates = new HashSet<int>(supplier.SuppliedChocolates.Select(c => c.ChocolateID));
            var viewModel = new List<SuppliedChocolateData>();
            foreach (var chocolate in allChocolates)
            {
                viewModel.Add(new SuppliedChocolateData
                {
                    ChocolateID = chocolate.ID,
                    Name = chocolate.Name,
                    IsSupplied = supplierChocolates.Contains(chocolate.ID)
                });
            }
            ViewData["Chocolates"] = viewModel;
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedChocolates)
        {
            if (id == null)
            {
                return NotFound();
            }
            var supplierToUpdate = await _context.Suppliers
            .Include(i => i.SuppliedChocolates)
            .ThenInclude(i => i.Chocolate)
            .FirstOrDefaultAsync(i => i.ID == id);

            if (await TryUpdateModelAsync<Supplier>(
            supplierToUpdate,
            "",
            i => i.SupplierName, i => i.Adress))
            {
                UpdateSuppliedChocolates(selectedChocolates, supplierToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {

                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, ");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdateSuppliedChocolates(selectedChocolates, supplierToUpdate);
            PopulateSuppliedChocolateData(supplierToUpdate);
            return View(supplierToUpdate);
        }
        private void UpdateSuppliedChocolates(string[] selectedChocolates, Supplier supplierToUpdate)
        {
            if (selectedChocolates == null)
            {
                supplierToUpdate.SuppliedChocolates = new List<SuppliedChocolate>();
                return;
            }
            var selectedChocolatesHS = new HashSet<string>(selectedChocolates);
            var suppliedChocolates = new HashSet<int>
            (supplierToUpdate.SuppliedChocolates.Select(c => c.Chocolate.ID));
            foreach (var chocolate in _context.Chocolates)
            {
                if (selectedChocolatesHS.Contains(chocolate.ID.ToString()))
                {
                    if (!suppliedChocolates.Contains(chocolate.ID))
                    {
                        supplierToUpdate.SuppliedChocolates.Add(new SuppliedChocolate
                        {
                            SupplierID = supplierToUpdate.ID,
                            ChocolateID = chocolate.ID
                        });
                    }
                }
                else
                {
                    if (suppliedChocolates.Contains(chocolate.ID))
                    {
                        SuppliedChocolate chocolateToRemove = supplierToUpdate.SuppliedChocolates.FirstOrDefault(i => i.ChocolateID == chocolate.ID);
                        _context.Remove(chocolateToRemove);
                    }
                }
            }
        }

        // GET: Suppliers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupplierExists(int id)
        {
            return _context.Suppliers.Any(e => e.ID == id);
        }
    }
}
