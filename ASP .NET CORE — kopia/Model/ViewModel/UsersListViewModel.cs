using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Model.ViewModel
{
    public class UsersListViewModel
    {
        public List<IdentityUser> IdentityUserList { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
