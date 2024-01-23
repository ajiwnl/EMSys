using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [PrimaryKey(nameof(UserName), nameof(Password))]
    public class Credentials
    {
        [Key, Column(Order = 0)]
        [Required]
        [StringLength(15)]
        [DisplayName("Username")]
        public string? UserName { get; set; }

        [Required]
        [Key, Column(Order = 1)]
        [DisplayName("Password")]
        public string? Password { get; set; }

        [Required]
        [DisplayName("Profile Icon")]
        public int? ProfileIcon { get; set; }
    }
}
