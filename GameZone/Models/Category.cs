namespace GameZone.Models
{
    public class Category:BaseModel
    {
        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}
