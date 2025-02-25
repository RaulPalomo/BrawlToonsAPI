using System.ComponentModel.DataAnnotations.Schema;
namespace BrawlToonsAPI.Models
{
    public class Matches
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int match_id { get; set; }
        public int player_1_id { get; set; }
        public int player_2_id { get; set; }
        public int winner_id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime date { get; set; }
    }
}
