using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingApp.Models
{
    public class Prenotazione
    {
        public int PrenotazioneId { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [Required]
        public int CameraId { get; set; }

        [Required]
        public DateTime DataInizio { get; set; }

        [Required]
        public DateTime DataFine { get; set; }

        public string Stato { get; set; }

        // Aggiunta per il tipo di camera
        public string TipoCamera { get; set; }

        // Aggiunta per il prezzo della camera
        public decimal Prezzo { get; set; }  // Prezzo della prenotazione

        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }

        [ForeignKey("CameraId")]
        public Camera Camera { get; set; }
    }
}
