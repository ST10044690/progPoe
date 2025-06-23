using System.ComponentModel.DataAnnotations;

namespace Agri_Energy_Connect.Models.Entities
{
    public class Farmer
    {

        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required, StringLength(100)]
        public string FarmName { get; set; }

        [Required, StringLength(200)]
        public string Address { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public DateTime JoinDate { get; set; }

        // Navigation properties
        public User User { get; set; }
        public List<Product> Products { get; set; }
    }
}
