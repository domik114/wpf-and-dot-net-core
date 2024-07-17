using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KinoBase.Models
{
    public class Film
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Tytul { get; set; }
        [Required]
        public int Rok { get; set; }
        [Required]
        public string Gatunek { get; set; }
        [Required]
        public string Rezyser { get; set; }
        [Required]
        public string Kraj { get; set; }

        
    }
}
