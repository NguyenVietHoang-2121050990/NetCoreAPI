using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MvcMovie.Models
{
    public class DaiLy
    {
        public string? MaDaiLy { get; set; }
        public string? TenDaiLy { get; set; }
        public string? DiaChi { get; set; }
        public string? NguoiDaiDien { get; set; }
        public string? DienThoai { get; set; }
        public string? MaHTPP { get; set; }

        // Liên kết với HeThongPhanPhoi
        public HeThongPhanPhoi? HeThongPhanPhoi { get; set; }
    }
}
