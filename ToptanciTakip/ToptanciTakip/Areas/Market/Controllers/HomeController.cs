using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ToptanciTakip.Areas.Market.Controllers
{
    [Area("Market")]
    [Authorize(Roles = "Market")]

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
