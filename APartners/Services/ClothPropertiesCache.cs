using APartners.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APartners.Services
{
    public static class ClothPropertiesCache
    {
        public static List<AColor>? AColors { get; set; }
        public static List<ASize>? ASizes { get; set; }
        public static List<ACategory>? ACategories { get; set; }
        public static List<AGender>? AGenders { get; set; }
    }
}
