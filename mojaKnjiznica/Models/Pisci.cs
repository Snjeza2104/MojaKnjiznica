using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mojaKnjiznica.Models
{
    public class Pisci
    {

        [Key]
        public int Id { get; set; }
        public int AutorId { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("AutorId")]
        public virtual Autori Autori { get; set; }
        
        public int KnjigaId { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("KnjigaId")]
        public virtual Knjiga Knjiga { get; set; }
    }
}