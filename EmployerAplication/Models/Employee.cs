using System.ComponentModel.DataAnnotations;

namespace EmployerAplication.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        [StringLength(13, MinimumLength = 12, ErrorMessage = "RFC must have beetwen 12 and 13 characters")]
        public string RFC { get; set; }
        public DateTime BornDate { get; set; }
        public EmployeeStatus Status { get; set; }
    }

    public enum EmployeeStatus
    {
        NotSet,
        Active,
        Inactive,
    }
}
