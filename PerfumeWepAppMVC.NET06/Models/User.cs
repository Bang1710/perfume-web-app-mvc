using System.ComponentModel.DataAnnotations;

namespace PerfumeWepAppMVC.NET06.Models
{
    public class User
    {
        [Key]
        public int User_ID { get; set; }

        [Required(ErrorMessage = "Tên người dùng không được bỏ trống, độ dài tối đa là 50 ký tự")]
        [MaxLength(50)]
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string User_Name { get; set;}
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [Required(ErrorMessage = "Mật khẩu không được bỏ trống, mật khẩu tối đa 20 ký tự")]
        [MaxLength(20)]
        [DataType(DataType.Password)]
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string User_Password { get; set;}
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [Required(ErrorMessage = "Email không được bỏ trống, độ dài tối đa 50 ký tự và theo đinh dạng abc@...")]
        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string User_Email { get; set;}
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [DataType(DataType.PhoneNumber)]
        [MaxLength(10)]
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string? User_PhoneNumber { get; set;}
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [MaxLength(100)]
        public string? User_Address { get; set;}

        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public virtual Cart Cart { get; set; }
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    }
}
