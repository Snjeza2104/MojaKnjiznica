using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mojaKnjiznica.Models
{
    public class PosudiliSu
    {
        [Key]
        public int Id { get; set; }
        public int PosidjivacId { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("PosidjivacId")]
        public virtual Posidjivac Posidjivac { get; set; }
        
        public int KnjigaId { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("KnjigaId")]
        public virtual Knjiga Knjiga { get; set; }
        [Required]
        public DateTime DatumPosudbe { get; set; }

    }
}