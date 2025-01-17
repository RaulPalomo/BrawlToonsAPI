namespace BrawlToonsAPI.Models
{
    public class Matches
    {
        public int match_id { get; set; }
        public int player_1_id { get; set; }
        public int player_2_id { get; set; }
        public int winner_id { get; set; }
        public DateTime date { get; set; }
    }
}