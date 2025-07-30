using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AitukCore.Contracts
{
    public class CategoryContract
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
