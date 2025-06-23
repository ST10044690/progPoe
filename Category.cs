using System.ComponentModel.DataAnnotations;

namespace Agri_Energy_Connect.Models.Entities
{
    public class Category
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        [StringLength(500)]
        public string Description { get; set; }

        // Navigation property
        public List<Product> Products { get; set; }
    }
}
