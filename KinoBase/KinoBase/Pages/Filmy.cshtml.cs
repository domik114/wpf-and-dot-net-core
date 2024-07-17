using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KinoBase.Data;
using KinoBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KinoBase.Pages
{
    public class FilmyModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public FilmyModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public IList<Film> Film { get; set; }
        public async Task<IActionResult> OnGet()
        {
            Film = await _db.Film.ToListAsync();
            return Page();
        }
        
        

    }
}
