using FinalCIS174.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalCIS174.Controllers
{
    public class PlayerController : Controller
    { 
        private DNDContext context;

        public PlayerController(DNDContext ctx)
        {
            context = ctx;
        }
    
        public IActionResult Index()
        {
            return View();
        }
    }
}
