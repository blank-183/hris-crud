using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace HRIS.Models
{
    public class UpdateEmployee
    {
        public int EmployeeId { get; set; }

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
        public string? Gsisno { get; set; }

        [MinLength(12, ErrorMessage = "Pag-Ibig No. must have 12 digits")]
        [MaxLength(12, ErrorMessage = "Pag-Ibig No. must only have 12 digits")]
        public string? PagIbigNo { get; set; }

        [MinLength(12, ErrorMessage = "PhilHealth No. must have 12 digits")]
        [MaxLength(12, ErrorMessage = "PhilHealth No. must only have 12 digits")]
        public string? PhilHealthNo { get; set; }

        [MinLength(10, ErrorMessage = "SSS No. must have 10 digits")]
        [MaxLength(10, ErrorMessage = "SSS No. must only have 10 digits")]
        public string? Sssno { get; set; }

        [MinLength(12, ErrorMessage = "TIN must have 12 digits")]
        [MaxLength(12, ErrorMessage = "TIN must only have 12 digits")]
        public string? Tinno { get; set; }
    }
}
