// HeThongPhanPhoi.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MvcMovie.Models
{
    public class HeThongPhanPhoi
    {
        public string? MaHTPP { get; set; }
        public string? TenHTPP { get; set; }
    }
}
