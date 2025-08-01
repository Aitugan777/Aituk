﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HaveServer.Models
{
    public class AProduct
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Cost { get; set; }

        public int Count { get; set; }

        [ForeignKey("AShop")]
        public int ShopId { get; set; }

        [JsonIgnore]
        public AShop? Shop { get; set; }

        public int CategoryId { get; set; }
    }
}
