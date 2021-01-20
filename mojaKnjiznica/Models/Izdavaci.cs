using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mojaKnjiznica.Models
{
    public class Izdavaci
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(25, ErrorMessage = "Naziv ne smije biti veći od 25 znakova.")]
        public string Naziv { get; set; }
        [StringLength(100)]
        public string Adresa { get; set; }
        [StringLength(20)]
        public string Mobitel { get; set; }
        [StringLength(30)]
        public string Email { get; set; }
    }
}