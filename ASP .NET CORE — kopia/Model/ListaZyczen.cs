using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Model
{
    public class ListaZyczen
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Tytul { get; set; }
        [Required]
        public string Firma { get; set; }
        [Required]
        public string Gatunek { get; set; }
        [Required]
        public int Ocena { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int GraId { get; set; }
    }
}
