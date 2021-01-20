using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mojaKnjiznica.Models
{
    public class Posidjivac
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Ime ne smije biti veće od 20 znakova")]
        public string Ime { get; set; }
        [Required]
        [StringLength(20)]
        public string Mobitel { get; set; }
        [Required]
        [StringLength(30)]
        public string Email { get; set; }
    }
}