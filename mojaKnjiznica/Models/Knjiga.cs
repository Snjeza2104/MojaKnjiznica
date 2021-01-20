using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mojaKnjiznica.Models
{
    public class Knjiga
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Naziv knjige ne smije biti dulji od 100 znakova")]
        public string Naslov { get; set; }
        [Required]
        public int IdIzdavac { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("IdIzdavac")]
        public virtual Izdavaci Izdavac { get; set; }
        public int BrojStranica { get; set; }
        public decimal Cijena { get; set; }
        [Required]
        public int GodinaIzdanja { get; set; }
    }
}