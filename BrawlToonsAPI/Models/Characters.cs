namespace BrawlToonsAPI.Models
{
    public class Characters
    {
        public int character_id { get; set; }
        public string? name { get; set; }
        public int total_wins { get; set; }
        public int total_defeats { get; set; }
    }
}