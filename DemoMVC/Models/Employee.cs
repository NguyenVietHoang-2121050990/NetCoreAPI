using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MvcMovie.Models
{
    public class Employee
    {
        public string? EmployeeId { get; set; }
        public int Age { get; set; }
    }
} 