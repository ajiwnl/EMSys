using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [PrimaryKey(nameof(ENRHFSTUDID))]
    public class Enrollment
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long? ENRHFSTUDID { get; set; }

        [Required]
        public DateTime? ENRHFSTUDDATEENROLL { get; set; }

        [Required]
        [StringLength(15)]
        public string? ENRHFSTUDSCHLYR { get; set; }

        [Required]
        [StringLength(15)]
        public string? ENRHFSTUDENCODER { get; set; }

        [Required]
        public double? ENRHFSTUDTOTALUNITS { get; set; }

        [Required]
        [StringLength(2)]
        public string? ENRHFSTUDSTATUS { get; set; }
    }
}
