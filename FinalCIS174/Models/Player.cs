namespace FinalCIS174.Models
{
    public class Player
    {
        public string PlayerID { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public string ClassID { get; set; }
        public Class? Class { get; set; }
        public string RaceID { get; set; }
        public Race? Race { get; set; }
    }
}
