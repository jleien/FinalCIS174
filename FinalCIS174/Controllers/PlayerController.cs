using FinalCIS174.Models;
using FinalCIS174.Repository;
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
        public PlayerController(DNDContext ctx) => context = ctx;
        //route
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

            model.Players = query.Where(c => c.CreatorOfCharacter.Equals(User.Identity.Name)).ToList();

            return View(model);
        }
        [Route("{controller}/{action}/{id?}")]
        public IActionResult Details(string id)
        {
            var session = new PlayerSession(HttpContext.Session);
            var model = new PlayerViewModel
            {
                Player = context.Players.Include(c => c.Race).Include(c => c.Class).FirstOrDefault(c => c.PlayerID == id),
                ActiveRace = session.GetActiveRace(),
                ActiveClass = session.GetActiveClass(),
            };
            if(model.Player.CreatorOfCharacter == User.Identity.Name)
            {
                return View(model);
            }
            else
            {
                TempData["message"] = "This is not your character.";
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        //add party members
        public RedirectToActionResult Add(PlayerViewModel model)
        {
            model.Player = context.Players.Include(c => c.Race).
                Include(c => c.Class).Where(c => c.PlayerID == model.
                Player.PlayerID).FirstOrDefault();

            var session = new PlayerSession(HttpContext.Session);
            var players = session.GetMyPlayers();
            bool findChar = false;
            //Stop duplication method
            foreach (var player in players)
            {
                //If dupe is found set to true and break out of loop
                if (player.PlayerID == model.Player.PlayerID)
                {
                    findChar = true;
                    break;
                }
                else
                {
                    findChar = false;
                }
            }

            if (findChar)
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
            var data = context.Players.ToList();
            //Auto increment player id
            IQueryable<Player> query = context.Players;
            List<int> findMaxID = new List<int>();
            foreach(var getPlayer in query)
            {
                int id = Convert.ToInt16(getPlayer.PlayerID);
                findMaxID.Add(id);
            }
            
            var playerID = findMaxID.Max() + 1;

            player.PlayerID = Convert.ToString(playerID);
            player.CreatorOfCharacter = User.Identity.Name;
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
        [Route("{controller}/{action}/{id?}")]
        public ActionResult EditPlayer(string PlayerID)
        {
            var data = context.Players.Where(x => x.PlayerID == PlayerID).FirstOrDefault();
            if(data.CreatorOfCharacter == User.Identity.Name)
            {
                ViewBag.Classes = context.Classes.ToList();
                ViewBag.Races = context.Races.ToList();
                return View(data);
            }
            else
            {
                TempData["message"] = "This is not your character.";
                return RedirectToAction("Index");
            }
            
        }
        

        [HttpPost]
        [Route("{controller}/{action}/{id?}")]
        public ActionResult EditPlayer(string PlayerID, Player model)
        {
            var data = context.Players.Where(x => x.PlayerID == PlayerID).FirstOrDefault();

            if (data != null)
            {
                data.PlayerID = model.PlayerID;
                data.Name = model.Name;
                data.Level = model.Level;
                data.ClassID = model.ClassID;
                data.RaceID = model.RaceID;
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
        [Route("{controller}/{action}/{PlayerID?}")]
        public ActionResult DeletePlayer(string PlayerID)
        {
            var data = context.Players.Where(x => x.PlayerID == PlayerID).FirstOrDefault();
            if (data.CreatorOfCharacter == User.Identity.Name)
            {
                ViewBag.Classes = context.Classes.ToList();
                ViewBag.Races = context.Races.ToList();
                return View(data);
            }
            else
            {
                TempData["message"] = "This is not your character.";
                return RedirectToAction("Index");
            }
            
        }
        [HttpPost]
        [Route("{controller}/{action}/{PlayerID?}")]
        public ActionResult DeletePlayer(string PlayerID, Player model)
        {
            var data = context.Players.Where(x => x.PlayerID == PlayerID).FirstOrDefault();

            if (data != null)
            {
                context.Remove(data);
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
