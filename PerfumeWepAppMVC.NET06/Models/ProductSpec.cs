using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PerfumeWepAppMVC.NET06.Models
{
    public class ProductSpec
    {
        [Key]
        [ForeignKey("Product")]
        [MaxLength(5)]
        public string Product_ID { get; set; }

        [Required]
        public string Product_Des { get; set; }

        [MaxLength(100)]
        public string Concentration { get; set; }

        [MaxLength(100)]
        public string Fragrance_Group { get; set; }

        [MaxLength(10)]
        public string Longevity { get; set; }

        [MaxLength(100)]
        public string Sillage { get; set; }

        [MaxLength(250)]
        public string Top_Note { get; set; }

        [MaxLength(250)]
        public string Middle_Note { get; set; }

        [MaxLength(250)]
        public string Base_Note { get; set; }

        [MaxLength(50)]
        public string Right_Time { get; set; }

        [MaxLength(50)]
        public string Recommended_Age { get; set; }

        public virtual Product Product { get; set; }
    }
}
