using HotelBookingApp.Data;
using HotelBookingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HotelBookingApp.Controllers
{
    [Authorize(Roles = "Admin,Employee")] // Limitato a utenti con ruoli Admin o Employee
    public class ClientiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Azione per visualizzare la lista dei clienti
        public IActionResult Index()
        {
            var clients = _context.Clienti.ToList(); // Recupera tutti i clienti
            return View(clients);
        }

        // Azione per creare un nuovo cliente
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // Azione per modificare un cliente
        public IActionResult Edit(int id)
        {
            var client = _context.Clienti.FirstOrDefault(c => c.Id == id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    _context.SaveChanges();
                }
                catch
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // Azione per eliminare un cliente
        public IActionResult Delete(int id)
        {
            var client = _context.Clienti.FirstOrDefault(c => c.Id == id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var client = _context.Clienti.FirstOrDefault(c => c.Id == id);
            _context.Clienti.Remove(client);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
