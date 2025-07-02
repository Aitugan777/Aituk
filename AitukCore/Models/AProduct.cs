using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace AitukCore.Models
{
    public class AProduct
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Cost { get; set; }

        public int Count { get; set; }

        [ForeignKey("AShop")]
        public long ShopId { get; set; }

        [JsonIgnore]
        public AShop? Shop { get; set; }

        public long CategoryId { get; set; }

        public List<APhoto> Photos { get; set; }
    }
}
