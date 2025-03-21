using Microsoft.AspNetCore.Identity;

namespace HotelBookingApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Proprietà personalizzate per i dipendenti dell'hotel
        public string Nome { get; set; }  // Nome del dipendente
        public string Cognome { get; set; }  // Cognome del dipendente
        public string Ruolo { get; set; }  // Ruolo nel sistema (es. "Receptionist", "Manager")

        // Costruttore vuoto richiesto da Identity
        public ApplicationUser() { }
    }
}
