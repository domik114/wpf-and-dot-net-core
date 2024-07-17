using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KinoBase.Data;
using KinoBase.Models;

namespace KinoBase.Pages
{
    public class UsunModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public UsunModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Film Film { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Film = await _db.Film.FirstOrDefaultAsync(m => m.ID == id);

            if (Film == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Film = await _db.Film.FindAsync(id);

            if (Film != null)
            {
                _db.Film.Remove(Film);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage("Filmy");
        }
    }
}
