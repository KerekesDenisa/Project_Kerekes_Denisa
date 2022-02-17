using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_Kerekes_Denisa_SADE.Data;
using Project_Kerekes_Denisa_SADE.Models;
using Microsoft.AspNetCore.Authorization;

namespace Project_Kerekes_Denisa_SADE.Controllers
{
    [Authorize(Roles = "Employee")]
    public class ChocolatesController : Controller
    {
        private readonly ShopContext _context;

        public ChocolatesController(ShopContext context)
        {
            _context = context;
        }

        // GET: Chocolates
        [AllowAnonymous]
        public async Task<IActionResult> Index(
                                              string sortOrder,
                                              string currentFilter,
                                              string searchString,
                                              int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";
            ViewData["WeightSortParm"] = sortOrder == "Weight" ? "weight_desc" : "Weight";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            var chocolates = from b in _context.Chocolates
                        select b;
            if (!String.IsNullOrEmpty(searchString))
            {
                chocolates = chocolates.Where(s => s.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    chocolates = chocolates.OrderByDescending(b => b.Name);
                    break;
                case "Price":
                    chocolates = chocolates.OrderBy(b => b.Price);
                    break;
                case "price_desc":
                    chocolates = chocolates.OrderByDescending(b => b.Price);
                    break;
                case "Weight":
                    chocolates = chocolates.OrderBy(b => b.Weight);
                    break;
                case "weight_desc":
                    chocolates = chocolates.OrderByDescending(b => b.Weight);
                    break;
                default:
                    chocolates = chocolates.OrderBy(b => b.Name);
                    break;
            }
            int pageSize = 2;
            return View(await PaginatedList<Chocolate>.CreateAsync(chocolates.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Chocolates/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chocolate = await _context.Chocolates
                .Include(s => s.Orders)
                .ThenInclude(e => e.Customer)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (chocolate == null)
            {
                return NotFound();
            }

            return View(chocolate);
        }

        // GET: Chocolates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Chocolates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Flavour,Weight,Price")] Chocolate chocolate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(chocolate);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            catch (DbUpdateException /* ex*/)
            {

                ModelState.AddModelError("", "Unable to save changes. " +
                "Try again, and if the problem persists ");
            }
            return View(chocolate);
        }

        // GET: Chocolates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chocolate = await _context.Chocolates.FindAsync(id);
            if (chocolate == null)
            {
                return NotFound();
            }
            return View(chocolate);
        }

        // POST: Chocolates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var chocolateToUpdate = await _context.Chocolates.FirstOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<Chocolate>(
            chocolateToUpdate,
            "",
            s => s.Name, s => s.Flavour, s => s.Price))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists");
                }
            }
            return View(chocolateToUpdate);
        }

        // GET: Chocolates/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chocolate = await _context.Chocolates
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (chocolate == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                "Delete failed. Try again";
            }

            return View(chocolate);
        }

        // POST: Chocolates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chocolate = await _context.Chocolates.FindAsync(id);
            if (chocolate == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try 
            { 
            _context.Chocolates.Remove(chocolate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {

                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool ChocolateExists(int id)
        {
            return _context.Chocolates.Any(e => e.ID == id);
        }
    }
}
