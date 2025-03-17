using System.ComponentModel.DataAnnotations;
namespace MvcDemo.Models
{
    public class Employee : Person 
    {
        public string EmployeeID { get; set; }= default!;
        public string Company { get; set; }= default!;

        public string GioiTinh { get; set; } = default!;
    }
}