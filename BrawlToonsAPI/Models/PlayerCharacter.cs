namespace BrawlToonsAPI.Models
{
    public class PlayerCharacter
    {
        public int player_id { get; set; }
        public int character_id { get; set; }
        public int wins { get; set; }
        public int defeats { get; set; }
    }
}