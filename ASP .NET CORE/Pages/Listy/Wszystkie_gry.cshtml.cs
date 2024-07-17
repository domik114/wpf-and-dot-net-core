using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Model;
using WebApplication1.Model.ViewModel;
using WebApplication1.Utility;

namespace WebApplication1.Pages.Listy
{
    public class Wszystkie_gryModel : PageModel
    {
        public IEnumerable<Gra> Gry { get; set; }
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public ListaZyczen ListaZyczen { get; set; }
        [BindProperty]
        public ListaPowrotu ListaPowrotu { get; set; }
        [BindProperty]
        public ListaZakazana ListaZakazana { get; set; }
        [BindProperty]
        public GraListViewModel GraListVM { get; set; }
        [BindProperty]
        public List<Gra> GraList { get; set; }

        public Wszystkie_gryModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> OnGet(int productPage = 1, string searchTytul = null, string searchFirma = null,
            string searchGatunek = null, int searchOcena = -1)
        {
            Gry = await _db.Gra.ToListAsync();

            GraListVM = new GraListViewModel()
            {
                GraList = await _db.Gra.ToListAsync()
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
                GraListVM.GraList = await _db.Gra.Where(u => u.Tytul.ToLower().Contains(searchTytul.ToLower())).ToListAsync();
            else
            {
                if (searchFirma != null)
                    GraListVM.GraList = await _db.Gra.Where(u => u.Firma.ToLower().Contains(searchFirma.ToLower())).ToListAsync();
                else
                {
                    if (searchGatunek != null)
                        GraListVM.GraList = await _db.Gra.Where(u => u.Gatunek.ToLower().Contains(searchGatunek.ToLower())).ToListAsync();
                    else
                    {
                        if (searchOcena != -1)
                            GraListVM.GraList = await _db.Gra.Where(u => u.Ocena.ToString().ToLower().Contains(searchOcena.ToString().ToLower())).ToListAsync();
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
            var gra = await _db.Gra.FindAsync(id);

            if (gra == null)
                return NotFound();

            _db.Gra.Remove(gra);
            await _db.SaveChangesAsync();

            return RedirectToPage("/Listy/Wszystkie_gry");
        }

        public async Task<IActionResult> OnPostZyczen(int id)
        {
            var gra1 = await _db.Gra.FindAsync(id);
            var gra3 = await (from n in _db.ListaZyczen select n).ToListAsync();

            foreach(var item in gra3)
            {
                if (item.UserId == User.Identity.Name && item.GraId == gra1.Id)
                {
                    // mesed¿ ze juz jest w bazie
                    return RedirectToPage("/Listy/Wszystkie_gry");
                }
            }

            if (true)
            {        
                ListaZyczen.Id = 0;
                ListaZyczen.Tytul = gra1.Tytul;
                ListaZyczen.Firma = gra1.Firma;
                ListaZyczen.Gatunek = gra1.Gatunek;
                ListaZyczen.Ocena = gra1.Ocena;
                ListaZyczen.UserId = User.Identity.Name;
                ListaZyczen.GraId = id;

                await _db.ListaZyczen.AddAsync(ListaZyczen);
                await _db.SaveChangesAsync();

                return RedirectToPage("/Listy/Lista_zyczen");
            }
        }

        public async Task<IActionResult> OnPostPowrotu(int id)
        {
            var gra1 = await _db.Gra.FindAsync(id);
            var gra3 = await (from n in _db.ListaPowrotu select n).ToListAsync();

            foreach (var item in gra3)
            {
                if (item.UserId == User.Identity.Name && item.GraId == gra1.Id)
                {
                    return RedirectToPage("/Listy/Wszystkie_gry");
                }
            }

            if (true)
            {
                ListaPowrotu.Id = 0;
                ListaPowrotu.Tytul = gra1.Tytul;
                ListaPowrotu.Firma = gra1.Firma;
                ListaPowrotu.Gatunek = gra1.Gatunek;
                ListaPowrotu.Ocena = gra1.Ocena;
                ListaPowrotu.UserId = User.Identity.Name;
                ListaPowrotu.GraId = id;

                await _db.ListaPowrotu.AddAsync(ListaPowrotu);
                await _db.SaveChangesAsync();

                return RedirectToPage("/Listy/Lista_powrotu");
            }
        }

        public async Task<IActionResult> OnPostZakazana(int id)
        {
            var gra1 = await _db.Gra.FindAsync(id);
            var gra3 = await (from n in _db.ListaZakazana select n).ToListAsync();

            foreach (var item in gra3)
            {
                if (item.UserId == User.Identity.Name && item.GraId == gra1.Id)
                {
                    return RedirectToPage("/Listy/Wszystkie_gry");
                }
            }

            if (true)
            {
                ListaZakazana.Id = 0;
                ListaZakazana.Tytul = gra1.Tytul;
                ListaZakazana.Firma = gra1.Firma;
                ListaZakazana.Gatunek = gra1.Gatunek;
                ListaZakazana.Ocena = gra1.Ocena;
                ListaZakazana.UserId = User.Identity.Name;
                ListaZakazana.GraId = id;

                await _db.ListaZakazana.AddAsync(ListaZakazana);
                await _db.SaveChangesAsync();

                return RedirectToPage("/Listy/Lista_zakazana");
            }
        }
    }
}
