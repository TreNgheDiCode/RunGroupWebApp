using Microsoft.AspNetCore.Mvc;
using RunGroupWebApp.Data;

namespace RunGroupWebApp.Controllers
{
    public class RaceController : Controller
    {
        private readonly DataContext _context;

        public RaceController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var races = _context.Races.ToList();
            return View(races);
        }
    }
}
