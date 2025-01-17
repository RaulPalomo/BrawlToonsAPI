namespace BrawlToonsAPI.Models
{
    public class Player
    {
        public int player_id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int games_played { get; set; } = 0;
        public int total_wins { get; set; } = 0;
        public int total_losses { get; set; } = 0;
    }
}
