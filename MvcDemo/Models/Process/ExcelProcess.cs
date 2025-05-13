using System.Data;
using System.IO;
using OfficeOpenXml;

namespace MvcDemo.Models.Process
{
    public class ExcelProcess
    {
        public DataTable ExcelToDataTable(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            using (var package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[1]; // EPPlus index starts at 1
                DataTable dt = new DataTable();

                // Tạo các cột từ hàng đầu tiên (header)
                for (int col = worksheet.Dimension.Start.Column; col <= worksheet.Dimension.End.Column; col++)
                {
                    dt.Columns.Add(worksheet.Cells[1, col].Text);
                }

                // Đọc từng dòng dữ liệu từ Excel
                for (int row = worksheet.Dimension.Start.Row + 1; row <= worksheet.Dimension.End.Row; row++)
                {
                    DataRow dr = dt.NewRow();
                    for (int col = worksheet.Dimension.Start.Column; col <= worksheet.Dimension.End.Column; col++)
                    {
                        dr[col - 1] = worksheet.Cells[row, col].Text;
                    }
                    dt.Rows.Add(dr);
                }

                return dt;
            }
        }
    }
}
