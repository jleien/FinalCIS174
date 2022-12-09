using FinalCIS174.Models;

namespace FinalCIS174.Repository
{
    public interface IPlayerRepository<T> where T : class
    {
        List<Player> GetAllPlayers();
        Player GetPlayerById(string id);
        void Save();
        void AddPlayer(Player player);
        void DeletePlayer(Player player);
        void EditPlayer(Player player);
        int Count();
    }
}
