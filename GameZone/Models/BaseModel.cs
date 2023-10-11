using System.ComponentModel.DataAnnotations;

namespace GameZone.Models
{
    public class BaseModel
    {
        public int ID { get; set; }
        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;
    }
}
