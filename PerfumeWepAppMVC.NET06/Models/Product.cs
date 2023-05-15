using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PerfumeWebApp.NET06.Data;

namespace PerfumeWepAppMVC.NET06.Models
{


    public class Product
    {
        [Key]
        [MaxLength(5)]
        public string Product_ID { get; set; }

        [Required]
        [MaxLength(5)]
        [ForeignKey("Category")]
        public string Category_ID { get; set; }

        [Required]
        [MaxLength(500)]
        public string Product_Name { get; set; }

        [Required]
        public int Product_Price { get; set; }

        [Required]
        [MaxLength(20)]
        public string Product_Origin { get; set; }

        [Required]
        [MaxLength(10)]
        public string Product_Gender { get; set; }

        [Required]
        [MaxLength(50)]
        public string Product_Style { get; set; }

        [Required]
        public int Product_ReleaseYear { get; set; }

        [Required]
        [MaxLength(10)]
        public string Product_Volume { get; set; }

        [Required]
        public bool Product_IsNew { get; set; }

        [Required]
        public bool Product_IsRecommend { get; set; }

        [Required]
        public bool Product_IsTrending { get; set; }

        [Required]
        public string Product_Intro { get; set; }

        public virtual Category Category { get; set; }
        public virtual ProductSpec ProductSpec { get; set; }
        //public virtual ICollection<CartItem> CartItems { get; set; }
        //public virtual ICollection<Review> Reviews { get; set; }
    }

}
