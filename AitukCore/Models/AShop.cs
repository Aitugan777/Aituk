using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AitukCore.Models
{
    public class AShop
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public double PositionX { get; set; }

        public double PositionY { get; set; }

        [ForeignKey("ASeller")]
        public long SellerId { get; set; }

        [JsonIgnore]
        public ASeller? Seller { get; set; }

        [JsonIgnore]
        public List<AProduct>? Products { get; set; }

        public APhoto Photo {  get; set; }
    }
}
