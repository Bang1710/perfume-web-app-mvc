using System.ComponentModel.DataAnnotations;

namespace PerfumeWepAppMVC.NET06.Models
{
    public class Category
    {
        [Key]
        [MaxLength(5)]
        public string Category_ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Category_Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
