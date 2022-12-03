using FinalCIS174.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalCIS174.Controllers
{
    //Uncomment for players to have an account to use the app
    [Authorize]
    public class PartyController : Controller
    {
        [HttpGet]
        public ViewResult Index()
        {
            var session = new PlayerSession(HttpContext.Session);
            var model = new PlayerListViewModel
            {
                ActiveClass = session.GetActiveClass(),
                ActiveRace = session.GetActiveRace(),
                Players = session.GetMyPlayers()
            };

            return View(model);
        }

        [HttpPost]
        public RedirectToActionResult Delete()
        {
            var session = new PlayerSession(HttpContext.Session);
            var cookies = new PlayerCookies(Response.Cookies);

            session.RemoveMyPlayers();
            cookies.RemoveMyPlayerIds();

            TempData["message"] = "Party Cleared";

            return RedirectToAction("Index", "Player",
                new
                {
                    ActiveClass = session.GetActiveClass(),
                    ActiveRace = session.GetActiveRace(),
                });
        }
    }
}
