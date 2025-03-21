using System.ComponentModel.DataAnnotations;

namespace HotelBookingApp.Models
{
    public class Camera
    {
        public int CameraId { get; set; }

        [Required]
        public int Numero { get; set; }

        [Required]
        public string Tipo { get; set; }

        [Required]
        public decimal Prezzo { get; set; }
    }
}
