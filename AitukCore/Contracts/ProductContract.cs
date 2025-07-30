using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace AitukCore.Contracts
{
    public class ProductContract
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public List<ShopCompactContract> Shops { get; set; }
        public List<int> Sizes { get; set; }
        public int CategoryId { get; set; }
        public int ColorId { get; set; }
        public int GenderId { get; set; }
        public string Brand { get; set; }
        public string Code { get; set; }
        public string KeyWords { get; set; }
        public List<byte[]> Photos { get; set; }
    }
}
