using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace HRIS.Models
{
    public class AddEmployee
    {

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public string? NameExtension { get; set; }

        [Required]
        [DisplayName("Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Sex { get; set; }

        [Required]
        [DisplayName("Civil Status")]
        public string CivilStatus { get; set; }

        [Required]
        public double Height { get; set; }

        [Required]
        public double Weight { get; set; }

        [Required]
        [DisplayName("Blood Type")]
        public string BloodType { get; set; }

        [MinLength(11, ErrorMessage = "GSIS No. must have 11 digits")]
        [MaxLength(11, ErrorMessage = "GSIS No. must only have 11 digits")]
        public string? GsisNo { get; set; }

        [MinLength(12, ErrorMessage = "Pag-Ibig No. must have 12 digits")]
        [MaxLength(12, ErrorMessage = "Pag-Ibig No. must only have 12 digits")]
        public string? PagibigNo { get; set; }

        [MinLength(12, ErrorMessage = "PhilHealth No. must have 12 digits")]
        [MaxLength(12, ErrorMessage = "PhilHealth No. must only have 12 digits")]
        public string? PhilhealthNo { get; set; }

        [MinLength(10, ErrorMessage = "SSS No. must have 10 digits")]
        [MaxLength(10, ErrorMessage = "SSS No. must only have 10 digits")]
        public string? SssNo { get; set; }

        [MinLength(12, ErrorMessage = "TIN must have 12 digits")]
        [MaxLength(12, ErrorMessage = "TIN must only have 12 digits")]
        public string? Tin { get; set; }
    }
}
