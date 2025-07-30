using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HaveServer.Models
{
    public class AShop
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public double PositionX { get; set; }

        public double PositionY { get; set; }

        [ForeignKey("ASeller")]
        public int SellerId { get; set; }

        [JsonIgnore]
        public ASeller? Seller { get; set; }

        [JsonIgnore]
        public List<AProduct>? Products { get; set; }
    }
}
