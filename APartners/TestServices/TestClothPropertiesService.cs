using APartners.Models;
using APartners.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APartners.TestServices
{
    public class TestClothPropertiesService : IClothPropertiesService
    {
        public List<AColor> GetColors()
        {
            if (ClothPropertiesCache.AColors != null)
                return ClothPropertiesCache.AColors;

            var colors = new List<AColor>
            {
                new AColor { Id = 1, Name = "Красный" },
                new AColor { Id = 2, Name = "Синий" },
                new AColor { Id = 3, Name = "Зеленый" },
                new AColor { Id = 4, Name = "Черный" },
                new AColor { Id = 5, Name = "Белый" },
                new AColor { Id = 6, Name = "Желтый" },
                new AColor { Id = 7, Name = "Серый" },
                new AColor { Id = 8, Name = "Розовый" }
            };

            ClothPropertiesCache.AColors = colors;
            return colors;
        }

        public List<ASize> GetSizes()
        {
            if (ClothPropertiesCache.ASizes != null)
                return ClothPropertiesCache.ASizes;

            var sizes = new List<ASize>
            {
                new ASize { Id = 1, Name = "XS" },
                new ASize { Id = 2, Name = "S" },
                new ASize { Id = 3, Name = "M" },
                new ASize { Id = 4, Name = "L" },
                new ASize { Id = 5, Name = "XL" },
                new ASize { Id = 6, Name = "XXL" }
            };

            ClothPropertiesCache.ASizes = sizes;
            return sizes;
        }

        public List<ACategory> GetCategories()
        {
            if (ClothPropertiesCache.ACategories != null)
                return ClothPropertiesCache.ACategories;

            var categories = new List<ACategory>
            {
                new ACategory { Id = 1, Name = "Футболки" },
                new ACategory { Id = 2, Name = "Рубашки" },
                new ACategory { Id = 3, Name = "Брюки" },
                new ACategory { Id = 4, Name = "Джинсы" },
                new ACategory { Id = 5, Name = "Платья" },
                new ACategory { Id = 6, Name = "Куртки" },
                new ACategory { Id = 7, Name = "Обувь" }
            };

            ClothPropertiesCache.ACategories = categories;
            return categories;
        }

        public List<AGender> GetGenders()
        {
            if (ClothPropertiesCache.AGenders != null)
                return ClothPropertiesCache.AGenders;

            var genders = new List<AGender>
            {
                new AGender { Id = 1, Name = "Мужской" },
                new AGender { Id = 2, Name = "Женский" },
                new AGender { Id = 3, Name = "Унисекс" }
            };

            ClothPropertiesCache.AGenders = genders;
            return genders;
        }
    }

}
