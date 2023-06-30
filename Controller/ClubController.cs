using Microsoft.AspNetCore.Mvc;
using RunGroupWebApp.Data;

namespace RunGroupWebApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly DataContext _context;

        public ClubController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var clubs = _context.Clubs.ToList();
            return View(clubs);
        }
    }
}
