using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vehiculos.Models.Data;
using Vehiculos.Models.Owner;

namespace Vehiculos.Controllers
{
    public class OwnersController: Controller
    {
        private readonly ApplicationDbContext _context;

        public OwnersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Propietarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Propietarios.ToListAsync());
        }

        // GET: Propietarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Propietarios = await _context.Propietarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Propietarios == null)
            {
                return NotFound();
            }

            return View(Propietarios);
        }

        // GET: Propietarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Propietarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Document,DocumentType,Phone,Address")] Propietarios Propietarios)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Propietarios);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Propietarios);
        }

        // GET: Propietarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Propietarios = await _context.Propietarios.FindAsync(id);
            if (Propietarios == null)
            {
                return NotFound();
            }
            return View(Propietarios);
        }

        // POST: Propietarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Document,DocumentType,Phone,Address")] Propietarios Propietarios)
        {
            if (id != Propietarios.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Propietarios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropietariosExists(Propietarios.Id))
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
            return View(Propietarios);
        }

        // GET: Propietarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Propietarios = await _context.Propietarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Propietarios == null)
            {
                return NotFound();
            }

            return View(Propietarios);
        }

        // POST: Propietarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Propietarios = await _context.Propietarios.FindAsync(id);
            if (Propietarios != null)
            {
                _context.Propietarios.Remove(Propietarios);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropietariosExists(int id)
        {
            return _context.Propietarios.Any(e => e.Id == id);
        }
    }
}
