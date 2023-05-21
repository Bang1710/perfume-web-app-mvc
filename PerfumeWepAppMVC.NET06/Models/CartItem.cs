
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfumeWepAppMVC.NET06.Models
{
    public class CartItem
    {
        [Key]
        public int CartItems_ID { get; set; }

        [ForeignKey("Product")]
        [MaxLength(5)]
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Product_ID { get; set; }
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [ForeignKey("Cart")]
        public int Cart_ID { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [MaxLength(500)]
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Product_Name { get; set; }
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public int Product_Price { get; set; }

        public int TotalPrice => Product_Price * Quantity;

        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public virtual Cart Cart { get; set; } // Đối tượng Cart liên kết với CartItem
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public virtual Product Product { get; set; } // Đối tượng Product liên kết với CartItem
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
