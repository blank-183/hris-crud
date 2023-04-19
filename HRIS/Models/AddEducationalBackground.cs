using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HRIS.Models;

public class AddEducationalBackground
{
    public int EmployeeId { get; set; }

    [Required]
    public string Level { get; set; } = null!;

    [Required]
    public string NameOfSchool { get; set; } = null!;

    public string? Course { get; set; }

    [Required]
    public DateTime FromDate { get; set; }

    [Required]
    public DateTime ToDate { get; set; }

    public short? UnitsEarned { get; set; }

    public short? YearGraduated { get; set; }

    public string? HonorsReceived { get; set; }

}