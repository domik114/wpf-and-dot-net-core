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
    public class Edytuj_greModel : PageModel
    {
        [BindProperty]
        public Gra Gra { get; set; }
        private readonly ApplicationDbContext _db;

        public Edytuj_greModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task OnGet(int id)
        {
            Gra = await _db.Gra.FindAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var GraZBazy = await _db.Gra.FindAsync(Gra.Id);

                GraZBazy.Tytul = Gra.Tytul;
                GraZBazy.Firma = Gra.Firma;
                GraZBazy.Gatunek = Gra.Gatunek;
                GraZBazy.Ocena = Gra.Ocena;

                await _db.SaveChangesAsync();

                return RedirectToPage("/Listy/Wszystkie_gry");
            }

            return RedirectToPage();
        }
    }
}
