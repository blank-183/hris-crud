using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HRIS.Models;

[Table("Employee")]
public partial class Employee
{
    [Key]
    public int EmployeeId { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string FirstName { get; set; } = null!;

    [StringLength(30)]
    [Unicode(false)]
    public string? MiddleName { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string LastName { get; set; } = null!;

    [StringLength(5)]
    [Unicode(false)]
    public string? NameExtension { get; set; }

    [Column(TypeName = "date")]
    public DateTime DateOfBirth { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string Sex { get; set; } = null!;

    [StringLength(15)]
    [Unicode(false)]
    public string CivilStatus { get; set; } = null!;

    public double Height { get; set; }

    public double Weight { get; set; }

    [StringLength(3)]
    [Unicode(false)]
    public string BloodType { get; set; } = null!;

    [InverseProperty("Employee")]
    public virtual ICollection<EducationalBackground> EducationalBackgrounds { get; set; } = new List<EducationalBackground>();

    [InverseProperty("Employee")]
    public virtual GovernmentAccount? GovernmentAccount { get; set; }

    [NotMapped]
    public string FullName
    {
        get
        {
            if (MiddleName != null)
            {
                return FirstName + " " + MiddleName + " " + LastName;
            }

            return FirstName + " " + LastName;
        }
    }
}
