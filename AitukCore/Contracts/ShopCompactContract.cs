using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AitukCore.Contracts
{
    public class ShopCompactContract
    {
        public long Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Address { get; set; }

        public int ProductCount { get; set; }
    }
}
