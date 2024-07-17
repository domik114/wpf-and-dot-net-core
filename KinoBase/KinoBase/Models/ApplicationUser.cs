using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KinoBase.Models
{
    public class ApplicationUser : IdentityUser 
    {
        public string Login { get; set; }
        public string Imie { get; set; }
        public string Miasto { get; set; }
        public string Płec { get; set; }

    }
}
