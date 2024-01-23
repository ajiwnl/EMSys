using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [PrimaryKey(nameof(SSFEDPCODE))]
    public class SubjectSched
    {
		[Key]
		[Required]
		[StringLength(8)]
		[DisplayName("EDP Code")]
		public string? SSFEDPCODE { get; set; }

		[Required]
		[StringLength(10)]
		[DisplayName("Course Code")]
		public string? SSFCOURSECODE { get; set; }

		[Required]
		[StringLength(15)]
		[DisplayName("Subject Code")]
		public string? SSFSUBJCODE { get; set; }

		[Required]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm tt}")]
		[DisplayName("Start Time")]
		public DateTime SSFSTARTTIME { get; set; }

		[Required]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm tt}")]
		[DisplayName("End Time")]
		public DateTime SSFENDTIME { get; set; }

		[Required]
		[StringLength(3)]
		[DisplayName("Days")]
		public string? SSFDAYS { get; set; }

		[Required]
		[StringLength(3)]
		[DisplayName("Room")]
		public string? SSFROOM { get; set; }

		[Required]
		[DisplayName("Max Size")]
		public int? SSFMAXSIZE { get; set; }

		[Required]
		[DisplayName("Class Size")]
		public int? SSFCLASSSIZE { get; set; }

		[Required]
		[DisplayName("Status")]
		public string? SSFSTATUS { get; set; }

		[Required]
		[StringLength(3)]
		[DisplayName("AM/PM")]
		public string? SSFXM { get; set; }

		[Required]
		[DisplayName("Section")]
		public string? SSFSECTION { get; set; }

		[Required]
		[DisplayName("Year")]
		public int? SSFSCHOOLYEAR { get; set; }
	}
}
