using Microsoft.AspNetCore.Mvc;

namespace SaleWebMvc.Controllers
{
    public class SellersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
