using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PerfumeWebApp.NET06.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace PerfumeWepAppMVC.NET06.Models
{

    public class Product
    {

        [Key]
        [Required(ErrorMessage = "Mã sản phẩm không được bỏ trống và độ dài tối đa 5 ký tự")]
        [MaxLength(5)]
        [DisplayName("Mã sản phẩm")]
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Product_ID { get; set; }
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [Required(ErrorMessage = "Mã loại sản phẩm không được bỏ trống, hãy chọn loại sản phẩm")]
        [MaxLength(5)]
        [ForeignKey("Category")]
        [DisplayName("Mã loại sản phẩm")]
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Category_ID { get; set; }
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [Required(ErrorMessage = "Tên sản phẩm không được bỏ trống")]
        [MaxLength(500)]
        [DisplayName("Tên sản phẩm")]
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Product_Name { get; set; }
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [Required(ErrorMessage = "Giá sản phẩm không được bỏ trống, và là kiểu số nguyên")]
        [DisplayName("Giá sản phẩm")]
        public int Product_Price { get; set; }

        [Required(ErrorMessage = "Xuất xứ sản phẩm không được bỏ trống, độ dài tối đa 20 ký tự")]
        [MaxLength(20)]
        [DisplayName("Xuất xứ")]
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Product_Origin { get; set; }
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [Required(ErrorMessage = "Giới tính sử dụng sản phẩm không được bỏ trống, độ dài tối đa 10 ký tự")]
        [MaxLength(10)]
        [DisplayName("Giới tính")]
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Product_Gender { get; set; }
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [Required(ErrorMessage = "Phong cách của sản phẩm không được bỏ trống, tối đa 50 ký tự")]
        [MaxLength(50)]
        [DisplayName("Phong cách")]
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Product_Style { get; set; }
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [Required(ErrorMessage = "Năm phát hành của sản phẩm không được bỏ trống")]
        [DisplayName("Năm phát hành")]
        public int Product_ReleaseYear { get; set; }

        [Required(ErrorMessage = "Dung tích sản phẩm không được bỏ trống ")]
        [MaxLength(10)]
        [DisplayName("Dung tích")]
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Product_Volume { get; set; }
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [Required]
        [DisplayName("Sản phẩm mới")]
        public bool Product_IsNew { get; set; }

        [Required]
        [DisplayName("Sản phẩm đề xuất")]
        public bool Product_IsRecommend { get; set; }

        [Required]
        [DisplayName("Sản phẩm xu hướng")]
        public bool Product_IsTrending { get; set; }

        [Required(ErrorMessage = "Giới thiệu sản phẩm không được bỏ trống")]
        [DisplayName("Giới thiệu sản phẩm")]
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Product_Intro { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        [DisplayName("Loại sản phẩm")]
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public virtual Category Category { get; set; }
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public virtual ProductSpec ProductSpec { get; set; }
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public virtual ICollection<CartItem> CartItems { get; set; }
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public virtual ICollection<ReviewProduct> ReviewProducts { get; set; }
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }

   


}
