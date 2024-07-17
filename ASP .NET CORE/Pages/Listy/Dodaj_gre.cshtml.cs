using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Data;
using WebApplication1.Model;
using WebApplication1.Utility;

namespace WebApplication1.Pages.Listy
{
    [Authorize(Roles = SD.AdminEndUser)]
    public class Dodaj_greModel : PageModel
    {
        [BindProperty]
        public Gra Gra { get; set; }
        private readonly ApplicationDbContext _db;

        public Dodaj_greModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await _db.Gra.AddAsync(Gra);
                await _db.SaveChangesAsync();

                return RedirectToPage("/Listy/Wszystkie_gry");
            }
            else
                return Page();
        }
    }
}
