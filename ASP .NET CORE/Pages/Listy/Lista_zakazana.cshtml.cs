using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Model;
using WebApplication1.Model.ViewModel;
using WebApplication1.Utility;

namespace WebApplication1.Pages.Listy
{
    public class Lista_zakazanaModel : PageModel
    {
        public IEnumerable<ListaZakazana> ListaZakazanas { get; set; }
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public ZakazanaListViewModel GraListVM { get; set; }
        [BindProperty]
        public List<ListaZyczen> GraList { get; set; }

        public Lista_zakazanaModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> OnGet(int productPage = 1, string searchTytul = null, string searchFirma = null,
            string searchGatunek = null, int searchOcena = -1)
        {
            ListaZakazanas = await _db.ListaZakazana.Where(u => u.UserId.ToLower().Contains(User.Identity.Name.ToLower())).ToListAsync();

            GraListVM = new ZakazanaListViewModel()
            {
                GraList = await _db.ListaZakazana.Where(u => u.UserId.ToLower().Contains(User.Identity.Name.ToLower())).ToListAsync()
            };

            StringBuilder param = new StringBuilder();
            param.Append("/Listy/Wszystkie_gry?productPage=:");

            param.Append("&searchTytul=");
            if (searchTytul != null)
                param.Append(searchTytul);

            param.Append("&searchFirma=");
            if (searchFirma != null)
                param.Append(searchFirma);

            param.Append("&searchGatunek=");
            if (searchGatunek != null)
                param.Append(searchGatunek);

            param.Append("&searchOcena=");
            if (searchOcena != -1)
                param.Append(searchOcena);

            if (searchTytul != null)
                GraListVM.GraList = await _db.ListaZakazana.Where(u => u.Tytul.ToLower().Contains(searchTytul.ToLower())).ToListAsync();
            else
            {
                if (searchFirma != null)
                    GraListVM.GraList = await _db.ListaZakazana.Where(u => u.Firma.ToLower().Contains(searchFirma.ToLower())).ToListAsync();
                else
                {
                    if (searchGatunek != null)
                        GraListVM.GraList = await _db.ListaZakazana.Where(u => u.Gatunek.ToLower().Contains(searchGatunek.ToLower())).ToListAsync();
                    else
                    {
                        if (searchOcena != -1)
                            GraListVM.GraList = await _db.ListaZakazana.Where(u => u.Ocena.ToString().ToLower().Contains(searchOcena.ToString().ToLower())).ToListAsync();
                    }
                }
            }

            var count = GraListVM.GraList.Count;

            GraListVM.PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = SD.PaginationUsersPageSize,
                TotalItems = count,
                UrlParam = param.ToString()
            };

            GraListVM.GraList = GraListVM.GraList.OrderBy(p => p.Tytul)
                .Skip((productPage - 1) * SD.PaginationUsersPageSize)
                .Take(SD.PaginationUsersPageSize).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var gra = await _db.ListaZakazana.FindAsync(id);

            if (gra == null)
                return NotFound();

            _db.ListaZakazana.Remove(gra);
            await _db.SaveChangesAsync();

            return RedirectToPage("/Listy/Lista_zakazana");
        }
    }
}
