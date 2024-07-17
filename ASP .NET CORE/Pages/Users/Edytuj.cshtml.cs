using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Utility;

namespace WebApplication1.Pages.Users
{
    [Authorize(Roles = SD.AdminEndUser)]
    public class EdytujModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public IdentityUser IdentityUserList { get; set; }

        public EdytujModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task OnGet(string id)
        {
            IdentityUserList = await _db.Users.FindAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var UserZBazy = await _db.Users.FindAsync(IdentityUserList.Id);

                UserZBazy.UserName = IdentityUserList.UserName;
                UserZBazy.Email = IdentityUserList.Email;
                UserZBazy.NormalizedUserName = IdentityUserList.UserName;
                UserZBazy.NormalizedEmail = IdentityUserList.Email;

                await _db.SaveChangesAsync();

                return RedirectToPage("/Users/Uzytkownicy");
            }

            return RedirectToPage();
        }
    }
}
