using DemoMVC.Models;
using Microsoft.AspNetCore.Mvc;

public class GradeController : Controller
{
    [HttpGet]
    public ActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Index(GradeModel model)
    {
        model.FinalScore = (model.ScoreA * 0.6) + (model.ScoreB * 0.3) + (model.ScoreC * 0.1);
        ViewBag.Message = $"Điểm tổng kết: {model.FinalScore:F2}";
        return View(model);
    }
}
