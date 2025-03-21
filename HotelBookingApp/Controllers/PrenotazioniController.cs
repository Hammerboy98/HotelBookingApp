using HotelBookingApp.Data;
using HotelBookingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

[Authorize(Roles = "Admin, Employee")]
public class PrenotazioniController : Controller
{
    private readonly ApplicationDbContext _context;

    public PrenotazioniController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Visualizza tutte le prenotazioni
    public async Task<IActionResult> Index()
    {
        var prenotazioni = await _context.Prenotazioni
            .Include(p => p.Cliente)
            .Include(p => p.Camera)
            .ToListAsync();
        return View(prenotazioni);
    }

    // Aggiungi una nuova prenotazione
    public IActionResult Create()
    {
        // Carica i clienti e le camere per il form
        ViewData["Clienti"] = _context.Clienti.ToList();
        ViewData["Camere"] = _context.Camere.ToList();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Prenotazione prenotazione)
    {
        if (ModelState.IsValid)
        {
            // Impostiamo lo stato della prenotazione come "Confermata"
            prenotazione.Stato = "Confermata";

            // Aggiungi i dettagli della camera
            var camera = await _context.Camere.FindAsync(prenotazione.CameraId);
            if (camera != null)
            {
                prenotazione.TipoCamera = camera.Tipo;  // Aggiungi il tipo della camera
                prenotazione.Prezzo = camera.Prezzo;    // Aggiungi il prezzo della camera
            }

            // Salva la prenotazione nel database
            _context.Add(prenotazione);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Se il modello non è valido, ricarica i dati per il form
        ViewData["Clienti"] = _context.Clienti.ToList();
        ViewData["Camere"] = _context.Camere.ToList();
        return View(prenotazione);
    }

    // Modifica una prenotazione esistente
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var prenotazione = await _context.Prenotazioni
            .Include(p => p.Cliente)
            .Include(p => p.Camera)
            .FirstOrDefaultAsync(m => m.PrenotazioneId == id);
        if (prenotazione == null)
        {
            return NotFound();
        }

        // Carica i clienti e le camere per il form
        ViewData["Clienti"] = _context.Clienti.ToList();
        ViewData["Camere"] = _context.Camere.ToList();
        return View(prenotazione);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("PrenotazioneId,ClienteId,CameraId,DataInizio,DataFine,Stato")] Prenotazione prenotazione)
    {
        if (id != prenotazione.PrenotazioneId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                // Aggiungi i dettagli della camera
                var camera = await _context.Camere.FindAsync(prenotazione.CameraId);
                if (camera != null)
                {
                    prenotazione.TipoCamera = camera.Tipo;  // Aggiungi il tipo della camera
                    prenotazione.Prezzo = camera.Prezzo;    // Aggiungi il prezzo della camera
                }

                // Modifica la prenotazione nel contesto
                _context.Update(prenotazione);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Prenotazioni.Any(e => e.PrenotazioneId == prenotazione.PrenotazioneId))
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

        // Se il modello non è valido, ricarica i dati per il form
        ViewData["Clienti"] = _context.Clienti.ToList();
        ViewData["Camere"] = _context.Camere.ToList();
        return View(prenotazione);
    }
}
