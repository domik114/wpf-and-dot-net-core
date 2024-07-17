using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KinoBase.Data;
using KinoBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KinoBase.Pages
{
    public class DodajFilmModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Film Film { get; set; }

        public DodajFilmModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(Film Film)
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            _db.Film.Add(Film);
            await _db.SaveChangesAsync();

            return RedirectToPage("Filmy");
        }
    }
}
