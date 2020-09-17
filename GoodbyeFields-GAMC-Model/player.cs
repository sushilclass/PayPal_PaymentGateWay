using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GoodbyeFields_GAMC_Model
{
    [Table("Player")]
    public class Player
    {
        [Key]
        public string PlayerId { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
    }
}
