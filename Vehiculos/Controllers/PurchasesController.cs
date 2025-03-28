using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vehiculos.Models.Data;
using Vehiculos.Models.Purchase;
using Vehiculos.Models.Supplier;

namespace Vehiculos.Controllers
{
    public class PurchasesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchasesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Purchases
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Purchases.Include(p => p.Supplier).Include(p => p.Vehicle);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Purchases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchases = await _context.Purchases
                .Include(p => p.Supplier)
                .Include(p => p.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchases == null)
            {
                return NotFound();
            }

            return View(purchases);
        }

        // GET: Purchases/Create
        public IActionResult Create()
        {
            ViewData["SupplierId"] = new SelectList(_context.Set<Suppliers>(), "Id", "Id");
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "Id");
            return View();
        }

        // POST: Purchases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VehicleId,SupplierId,PurchaseDate,PurchasePrice")] Purchases purchases)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchases);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SupplierId"] = new SelectList(_context.Set<Suppliers>(), "Id", "Id", purchases.SupplierId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "Id", purchases.VehicleId);
            return View(purchases);
        }

        // GET: Purchases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchases = await _context.Purchases.FindAsync(id);
            if (purchases == null)
            {
                return NotFound();
            }
            ViewData["SupplierId"] = new SelectList(_context.Set<Suppliers>(), "Id", "Id", purchases.SupplierId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "Id", purchases.VehicleId);
            return View(purchases);
        }

        // POST: Purchases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VehicleId,SupplierId,PurchaseDate,PurchasePrice")] Purchases purchases)
        {
            if (id != purchases.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchases);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchasesExists(purchases.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SupplierId"] = new SelectList(_context.Set<Suppliers>(), "Id", "Id", purchases.SupplierId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "Id", purchases.VehicleId);
            return View(purchases);
        }

        // GET: Purchases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchases = await _context.Purchases
                .Include(p => p.Supplier)
                .Include(p => p.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchases == null)
            {
                return NotFound();
            }

            return View(purchases);
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchases = await _context.Purchases.FindAsync(id);
            if (purchases != null)
            {
                _context.Purchases.Remove(purchases);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchasesExists(int id)
        {
            return _context.Purchases.Any(e => e.Id == id);
        }
    }
}
