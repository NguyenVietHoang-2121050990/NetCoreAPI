using DemoMVC.Models;
using Microsoft.AspNetCore.Mvc;

public class BillController : Controller
{
    [HttpGet]
    public ActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Index(BillModel model)
    {
        if (model.Quantity > 0 && model.Price > 0)
        {
            model.Total = model.Quantity * model.Price;
            ViewBag.Message = $"Tổng tiền: {model.Total:C}";
        }
        else
        {
            ViewBag.Message = "Vui lòng nhập số lượng và đơn giá hợp lệ!";
        }
        return View(model);
    }
}
