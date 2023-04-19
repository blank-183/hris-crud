using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HRIS.Models;

[Table("EducationalBackground")]
public partial class EducationalBackground
{
    [Key]
    public int EducationalBackgroundId { get; set; }

    public int EmployeeId { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string Level { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string NameOfSchool { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string? Course { get; set; }

    [Column(TypeName = "date")]
    public DateTime FromDate { get; set; }

    [Column(TypeName = "date")]
    public DateTime ToDate { get; set; }

    public short? UnitsEarned { get; set; }

    public short? YearGraduated { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? HonorsReceived { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("EducationalBackgrounds")]
    public virtual Employee Employee { get; set; } = null!;
}
