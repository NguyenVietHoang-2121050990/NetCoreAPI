using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcDemo.Data;
using MvcDemo.Models;
using MvcDemo.Models.Process;
using OfficeOpenXml;
using X.PagedList;
using X.PagedList.Mvc.Core;


namespace MvcDemo.Controllers
{
    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ExcelProcess _excelProcess;

        public PersonController(ApplicationDbContext context)
        {
            _context = context;
            _excelProcess = new ExcelProcess();
        }
        // GET: Person

public IActionResult Index(string searchString, int? page)
{
    ViewData["CurrentFilter"] = searchString;

    var persons = _context.Person.AsQueryable();

    if (!string.IsNullOrEmpty(searchString))
    {
        persons = persons.Where(p => p.Fullname.Contains(searchString));
    }

    int pageSize = 5;
    int pageNumber = page ?? 1;

    // Cách nhanh: gọi ToList() trước rồi mới phân trang
    var pagedList = persons.ToList().ToPagedList(pageNumber, pageSize);

    return View(pagedList);
}


        // Export Excel
        public IActionResult Download()
        {
            var fileName = "YourFileName.xlsx";
            using (var excelPackage = new ExcelPackage())
            {
                var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");

                // Header
                worksheet.Cells["A1"].Value = "PersonID";
                worksheet.Cells["B1"].Value = "FullName";
                worksheet.Cells["C1"].Value = "Address";

                // Data
                var personList = _context.Person.ToList();
                worksheet.Cells["A2"].LoadFromCollection(personList, true);

                var stream = new MemoryStream(excelPackage.GetAsByteArray());

                return File(stream,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    fileName);
            }
        }

        // GET: Person/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,Fullname,Address")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: Person/Upload
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null)
            {
                string fileExtension = Path.GetExtension(file.FileName).ToLower();
                if (fileExtension != ".xls" && fileExtension != ".xlsx")
                {
                    ModelState.AddModelError("", "Please choose a valid Excel file to upload (xls or xlsx).");
                }
                else
                {
                    var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;
                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "excels");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    var filePath = Path.Combine(folderPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // Đọc Excel
                    var dt = _excelProcess.ExcelToDataTable(filePath);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i].ItemArray.Length >= 3)
                        {
                            var ps = new Person
                            {
                                PersonId = Convert.ToInt32(dt.Rows[i][0]),
                                Fullname = dt.Rows[i][1]?.ToString() ?? "",
                                Address = dt.Rows[i][2]?.ToString() ?? ""
                            };
                            _context.Add(ps);
                        }
                    }
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

        private bool PersonExists(int id)
        {
            return _context.Person.Any(e => e.PersonId == id);
        }
    }
}
