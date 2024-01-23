using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class EnrollmentDetails
    {
        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [DisplayName("Student ID")]
        public long? ENRDFSTUDID { get; set; }

        [Key, Column(Order = 2)]
        [Required]
        [StringLength(8)]
        [DisplayName("EDP Code")]
        public string? ENRDFSTUDEDPCODE { get; set; }

        [Required]
        [StringLength(15)]
        [DisplayName("Subject Code")]
        public string? ENRDFSTUDSUBJCODE { get; set; }

        [Required]
        [StringLength(2)]
        [DisplayName("Status")]
        public string? ENRDFSTUDSTATUS { get; set; }
    }
}
