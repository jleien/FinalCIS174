namespace FinalCIS174.Models
{
    public class PlayerListViewModel : PlayerViewModel
    {
        public List<Player> Players { get; set; }
        private List<Class> classes;

        public List<Class> Classes
        {
            get => classes;
            set
            {
                classes = value;
                classes.Insert(0, new Class { ClassID = "all", Name = "all" });
            }
        }

        private List<Race> races;
        public List<Race> Races
        {
            get => races;
            set
            {
                races = value;
                races.Insert(0,
                    new Race { RaceID = "all", Name = "all" });
            }
        }
        public string CheckActiveClass(string c) =>
           c.ToLower() == ActiveClass.ToLower() ? "active" : "";

        public string CheckActiveRace(string d) =>
            d.ToLower() == ActiveRace.ToLower() ? "active" : "";
    }
    }

