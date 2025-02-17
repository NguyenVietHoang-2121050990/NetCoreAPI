using DemoMVC.Models;
using Microsoft.AspNetCore.Mvc;

public class BMIController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(double Height, double Weight)
    {
        if (Height <= 0 || Weight <= 0)
        {
            ViewBag.Message = "Chiều cao và cân nặng phải lớn hơn 0!";
            return View();
        }

        double bmi = Weight / (Height * Height);
        string result;

        if (bmi < 18.5)
            result = "Gầy";
        else if (bmi < 24.9)
            result = "Bình thường";
        else if (bmi < 29.9)
            result = "Thừa cân";
        else
            result = "Béo phì";

        ViewBag.Message = $"Chỉ số BMI: {bmi:F2} - {result}";
        return View();
    }
}
