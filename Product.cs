using System.ComponentModel.DataAnnotations;

namespace Agri_Energy_Connect.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int FarmerId { get; set; }

        [Required]
        public DateTime ProductionDate { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Range(0, 10000)]
        public decimal Quantity { get; set; }

        public string QuantityUnit { get; set; } 

        public decimal Price { get; set; }

        // Navigation properties
        public Farmer Farmer { get; set; }
        public Category Category { get; set; }
    }
}
