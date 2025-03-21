using System.ComponentModel.DataAnnotations;

namespace HotelBookingApp.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Cognome { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Telefono { get; set; }
    }
}
