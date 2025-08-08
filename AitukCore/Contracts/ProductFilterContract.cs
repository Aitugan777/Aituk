using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AitukCore.Contracts
{
    public class ProductFilterContract
    {
        public string? SearchText { get; set; }
        public List<int>? SizeIds { get; set; }
        public List<int>? ColorIds { get; set; }
        public List<int>? CategoryIds { get; set; }
        public int? GenderId { get; set; }
        public decimal? MinCost { get; set; }
        public decimal? MaxCost { get; set; }
    }
}
