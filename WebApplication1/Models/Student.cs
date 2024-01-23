using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [PrimaryKey(nameof(STFSTUDID))]
    public class Student
    {
        [Column(Order = 0)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("Student ID")]
        public long STFSTUDID { get; set; }

        [DisplayName("Last Name")]
        [Required]
        [StringLength(15)]
        public string? STFSTUDLNAME { get; set; }

        [DisplayName("First Name")]
        [Required]
        [StringLength(15)]
        public string? STFSTUDFNAME { get; set; }

        [DisplayName("Middle Name")]
        [Required]
        [StringLength(15)]
        public string? STFSTUDMNAME { get; set; }

        [DisplayName("Student Course")]
        [Required]
        [StringLength(10)]
        public string? STFSTUDCOURSE { get; set; }

        [Required]
        [DisplayName("Year")]
        public int STFSTUDYEAR { get; set; }

        [DisplayName("Student Remarks")]
        [Required]
        [StringLength(15)]
        public string? STFSTUDREMARKS { get; set; }

        [Required]
        [StringLength(2)]
        [DisplayName("Status")]
        public string? STFSTUDSTATUS { get; set; }
    }
}
