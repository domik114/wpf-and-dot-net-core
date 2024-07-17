using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    [Route("api/Gra")]
    [ApiController]
    public class GraController : Controller
    {
        private readonly ApplicationDbContext _db;

        public GraController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _db.Gra.ToList() });
        }
    }
}
