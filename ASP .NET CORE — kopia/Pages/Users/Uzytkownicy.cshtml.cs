using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Model;
using WebApplication1.Model.ViewModel;
using WebApplication1.Utility;

namespace WebApplication1.Pages.Users
{
    [Authorize(Roles = SD.AdminEndUser)]
    public class UzytkownicyModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public UsersListViewModel UsersListVM { get; set; }
        [BindProperty]
        public List<IdentityUser> IdentityUserList { get; set; }

        public UzytkownicyModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> OnGet(int productPage = 1, string searchUserName = null, string searchEmail = null)
        {
            UsersListVM = new UsersListViewModel()
            {
                IdentityUserList = await _db.Users.ToListAsync()
            };

            StringBuilder param = new StringBuilder();
            param.Append("/Users/Uzytkownicy?productPage=:");

            param.Append("&searchName=");
            if (searchUserName != null)
                param.Append(searchUserName);

            param.Append("&searchEmail=");
            if (searchEmail != null)
                param.Append(searchEmail);

            if (searchEmail != null)
                UsersListVM.IdentityUserList = await _db.Users.Where(u => u.Email.ToLower().Contains(searchEmail.ToLower())).ToListAsync();
            else
            {
                if (searchUserName != null)
                    UsersListVM.IdentityUserList = await _db.Users.Where(u => u.UserName.ToLower().Contains(searchUserName.ToLower())).ToListAsync();
            }

            var count = UsersListVM.IdentityUserList.Count;

            UsersListVM.PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = SD.PaginationUsersPageSize,
                TotalItems = count,
                UrlParam = param.ToString()
            };

            UsersListVM.IdentityUserList = UsersListVM.IdentityUserList.OrderBy(p => p.Email)
                .Skip((productPage - 1) * SD.PaginationUsersPageSize)
                .Take(SD.PaginationUsersPageSize).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostDelete(string id)
        {
            var user = await _db.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            _db.Users.Remove(user);
            await _db.SaveChangesAsync();

            return RedirectToPage("/Users/Uzytkownicy");
        }
    }
}
