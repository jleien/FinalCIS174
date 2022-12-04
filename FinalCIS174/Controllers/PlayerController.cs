using FinalCIS174.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Net.Sockets;

namespace FinalCIS174.Controllers
{
    //Uncomment for players to have an account to use the app
    [Authorize]

    public class PlayerController : Controller
    {
        private DNDContext context;

        public PlayerController(DNDContext ctx)
        {
            context = ctx;
        }
        [Route("{controller}/{action}/{activeClass?}/{activeRace?}/")]

        public IActionResult Index(PlayerListViewModel model)
        {
            model.Races = context.Races.ToList();
            model.Classes = context.Classes.ToList();

            var session = new PlayerSession(HttpContext.Session);
            session.SetActiveClass(model.ActiveClass);
            session.SetActiveRace(model.ActiveRace);

            int? count = session.GetMyPlayerCount();
            if (count == null)
            {
                var cookies = new PlayerCookies(Request.Cookies);
                string[] ids = cookies.GetMyPlayerIds();

                List<Player> myplayers = new List<Player>();
                if (ids.Length > 0)
                    myplayers = context.Players.Include(c => c.Race).Include(c => c.Class).Where(c => ids.Contains(c.PlayerID)).ToList();
                session.SetMyPlayers(myplayers);
            }

            IQueryable<Player> query = context.Players;
            if (model.ActiveClass != "all")
                query = query.Where(
                    t => t.Class.ClassID.ToLower() == model.ActiveClass.ToLower());
            if (model.ActiveRace != "all")
                query = query.Where(
                    t => t.Race.RaceID.ToLower() == model.ActiveRace.ToLower());
            model.Players = query.ToList();

            return View(model);
        }
        [Route("player/{action}/{id?}")]
        public IActionResult Details(string id)
        {
            var session = new PlayerSession(HttpContext.Session);
            var model = new PlayerViewModel
            {
                Player = context.Players.Include(c => c.Race).Include(c => c.Class).FirstOrDefault(c => c.PlayerID == id),
                ActiveRace = session.GetActiveRace(),
                ActiveClass = session.GetActiveClass(),
        };
            return View(model);
        }

        [HttpPost]
        //add party members
        public RedirectToActionResult Add(PlayerViewModel model)
        {
            model.Player = context.Players.Include(c => c.Race).Include(c => c.Class).Where(c => c.PlayerID == model.Player.PlayerID).FirstOrDefault();

            var session = new PlayerSession(HttpContext.Session);
            var players = session.GetMyPlayers();

            //cant find duplicates
            if (players != players.Distinct())
            {
                TempData["message"] = $"this is already a member of your party.";
            }
            else
            {
                players.Add(model.Player);
                session.SetMyPlayers(players);

                var cookies = new PlayerCookies(Response.Cookies);
                cookies.SetMyPlayerIds(players);

                TempData["message"] = $"{model.Player.Name} added to your party";
            }

   

            return RedirectToAction("Index",
                new
                {
                    ActiveGame = session.GetActiveRace(),
                    ActiveCat = session.GetActiveClass()
                });
        }

        public IActionResult AddPlayer()
        {
            var player = new Player();

            ViewBag.Classes = context.Classes.ToList();
            ViewBag.Races = context.Races.ToList();
            return View(player);
        }

        [HttpPost]
        public IActionResult AddPlayer(Player model)
        {
            if (ModelState.IsValid)
            {

                context.Players.Add(model);

                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Classes = context.Classes.ToList();
                ViewBag.Races = context.Races.ToList();
                return View(model);
            }

        }
        [NonAction]
        public string GetSlug(string name)
        {
            return name.Replace(' ', '-').ToLower();
        }
    }
}
