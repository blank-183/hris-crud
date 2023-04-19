namespace HRIS.Models
{
    public class EmployeeDetails
    {
        public Employee Employee { get; set; }

        public GovernmentAccount GovernmentAccount { get; set; }
        public IEnumerable<EducationalBackground> EducationalBackgrounds { get; set; }
    }
}
