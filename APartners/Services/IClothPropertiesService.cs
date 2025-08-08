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
        Task<List<AColor>> GetColorsAsync();
        Task<List<ASize>> GetSizesAsync();
        Task<List<ACategory>> GetCategoriesAsync();
        Task<List<AGender>> GetGendersAsync();
    }
}
