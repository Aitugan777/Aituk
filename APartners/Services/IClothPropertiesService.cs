using APartners.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APartners.Services
{
    public interface IClothPropertiesService
    {
        List<AColor> GetColors();
        List<ASize> GetSizes();
        List<ACategory> GetCategories();
        List<AGender> GetGenders();
    }

}
