namespace FinalCIS174.Models
{
    public class PlayerSession
    {
        private const string PlayersKey = "myplayers";
        private const string CountKey = "playercount";
        private const string RaceKey = "racekey";
        private const string ClassKey = "classkey";

        private ISession session { get; set; }
        public PlayerSession(ISession session)
        {
            this.session = session;
        }   

        public void SetMyPlayers(List<Player> players)
        {
            session.SetObject(PlayersKey, players);
            session.SetInt32(CountKey, players.Count); 
        }

        public List<Player> GetMyPlayers() => session.GetObject<List<Player>>(PlayersKey) ?? new List<Player>();

        public int? GetMyPlayerCount() => session.GetInt32(CountKey);

        public void SetActiveRace(string activeRace) => session.SetString(RaceKey, activeRace);

        public string GetActiveRace() => session.GetString(RaceKey);

        public void SetActiveClass(string activeClass) => session.SetString(ClassKey, activeClass);

        public string GetActiveClass() => session.GetString(ClassKey);

        public void RemoveMyPlayers()
        {
            session.Remove(PlayersKey);
            session.Remove(CountKey);
        }
    }
}
