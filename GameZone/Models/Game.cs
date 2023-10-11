using System.ComponentModel.DataAnnotations;

namespace GameZone.Models
{
    public class Game: BaseModel
    {
     
        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;
        [MaxLength(2500)]
        public string Cover { get; set; } = string.Empty;
        public int CategoryID { get; set; }
        public Category Category { get; set; } = default!;
        public ICollection<GameDevice> Devices { get; set; } = new List<GameDevice>();

    }
}
