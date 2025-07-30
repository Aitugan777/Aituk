using System.ComponentModel.DataAnnotations;

namespace HaveServer.Models
{
    public class ASeller
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public List<AShop> Shops { get; set; }
    }
}
