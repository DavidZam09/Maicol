using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vehiculos.Models.Data;
using Vehiculos.Models.Owner;
using Vehiculos.Models.Vehicle;

namespace Vehiculos.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VehiclesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            var vehicles = await _context.Vehicles.ToListAsync();

            // Cargar manualmente los dueños de los vehículos
            foreach (var vehicle in vehicles)
            {
                vehicle.Owner = await _context.Propietarios.FindAsync(vehicle.OwnerId);
            }

            return View(vehicles);
        }


        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicles = await _context.Vehicles
                .Include(v => v.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicles == null)
            {
                return NotFound();
            }

            return View(vehicles);
        }

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            var owners = _context.Propietarios.ToList();

            if (owners == null || !owners.Any())  // Verifica que haya datos en la lista
            {
                ModelState.AddModelError("", "No hay propietarios registrados.");
                ViewBag.Owners = new SelectList(new List<object>(), "Id", "Name"); // Evita el error de null
            }
            else
            {
                ViewBag.Owners = new SelectList(owners, "Id", "Name");
                Console.WriteLine($"Datos propietario: {ViewBag.Owners}");
            }

            return View();
        }


        // POST: Vehicles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Model, Brand, EngineCapacity, Type, Color, Price, OwnerId, Year, Plate, Status")] Vehicles vehicle)
        {
            Console.WriteLine("Ingresando al método Create"); // Verifica si entra al método
           
            if (ModelState.IsValid)
            {
                try
                {
                    Console.WriteLine($"Insertando vehículo: {vehicle.Model}, {vehicle.Brand}, {vehicle.Price}, {vehicle.OwnerId}");

                    _context.Add(vehicle);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Vehículo guardado correctamente");

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al guardar el vehículo: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("⚠️ ModelState no es válido. Errores encontrados:");

                // Mostrar todos los errores detalladamente
                foreach (var kvp in ModelState)
                {
                    string fieldName = kvp.Key; // Nombre del campo
                    var errors = kvp.Value.Errors; // Errores asociados al campo

                    foreach (var error in errors)
                    {
                        Console.WriteLine($"Campo: {fieldName} - Error: {error.ErrorMessage}");
                    }
                }

                // Mostrar los valores reales que llegan
                Console.WriteLine("Valores recibidos en el formulario:");
                Console.WriteLine($"Model: {vehicle.Model}");
                Console.WriteLine($"Brand: {vehicle.Brand}");
                Console.WriteLine($"Price: {vehicle.Price}");
                Console.WriteLine($"OwnerId: {vehicle.OwnerId}"); // Verificar si está llegando correctamente
            }

            ViewBag.Owners = new SelectList(_context.Propietarios, "Id", "Name");
            return View(vehicle);
        }




        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicles = await _context.Vehicles.FindAsync(id);
            if (vehicles == null)
            {
                return NotFound();
            }
            ViewData["OwnerId"] = new SelectList(_context.Propietarios, "Id", "Name", vehicles.OwnerId);
            return View(vehicles);
        }

        // POST: Vehicles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Model,Brand,EngineCapacity,Type,Color,Price,OwnerId,Year,Plate,Status")] Vehicles vehicles)
        {
            if (id != vehicles.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehiclesExists(vehicles.Id))
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
            ViewData["OwnerId"] = new SelectList(_context.Propietarios, "Id", "Name", vehicles.OwnerId);
            return View(vehicles);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicles = await _context.Vehicles
                .Include(v => v.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicles == null)
            {
                return NotFound();
            }

            return View(vehicles);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicles = await _context.Vehicles.FindAsync(id);
            if (vehicles != null)
            {
                _context.Vehicles.Remove(vehicles);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehiclesExists(int id)
        {
            return _context.Vehicles.Any(e => e.Id == id);
        }
    }
}
