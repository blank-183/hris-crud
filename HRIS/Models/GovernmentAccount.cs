using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HRIS.Models;

[Table("GovernmentAccount")]
[Index("EmployeeId", Name = "UQ__Governme__7AD04F1000E3D462", IsUnique = true)]
public partial class GovernmentAccount
{
    [Key]
    public int GovernmentAccountId { get; set; }

    public int EmployeeId { get; set; }

    [StringLength(11)]
    [Unicode(false)]
    public string? GsisNo { get; set; }

    [StringLength(12)]
    [Unicode(false)]
    public string? PagibigNo { get; set; }

    [StringLength(12)]
    [Unicode(false)]
    public string? PhilhealthNo { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? SssNo { get; set; }

    [StringLength(12)]
    [Unicode(false)]
    public string? Tin { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("GovernmentAccount")]
    public virtual Employee Employee { get; set; } = null!;
}
