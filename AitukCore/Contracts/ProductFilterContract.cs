using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AitukCore.Contracts
{
    public class ProductFilterContract
    {
        public long? ShopId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal? MinCost { get; set; }
        public decimal? MaxCost { get; set; }
    }
}
