using System.Diagnostics.Metrics;

namespace FinalCIS174.Models
{
    public class PlayerCookies
    {
        private const string PlayersKey = "myplayers";
        private const string Delimiter = "-";

        private IRequestCookieCollection requestCookies { get; set; }
        private IResponseCookies responseCookies { get; set; }

        public PlayerCookies(IRequestCookieCollection cookies)
        {
            requestCookies = cookies;
        }
        public PlayerCookies(IResponseCookies cookies)
        {
            responseCookies = cookies;
        }

        public void SetMyPlayerIds(List<Player> myplayers)
        {
            List<string> ids = myplayers.Select(c => c.PlayerID).ToList();
            string idsString = String.Join(Delimiter, ids);
            CookieOptions options = new CookieOptions { Expires = DateTime.Now.AddDays(30) };
            RemoveMyPlayerIds();     // delete old cookie first
            responseCookies.Append(PlayersKey, idsString, options);
        }

        public string[] GetMyPlayerIds()
        {
            string cookie = requestCookies[PlayersKey];
            if (string.IsNullOrEmpty(cookie))
                return new string[] { };   // empty string array
            else
                return cookie.Split(Delimiter);
        }

        public void RemoveMyPlayerIds()
        {
            responseCookies.Delete(PlayersKey);
        }
    }
}
