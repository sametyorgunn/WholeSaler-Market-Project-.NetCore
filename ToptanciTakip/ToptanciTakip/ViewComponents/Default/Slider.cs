using Microsoft.AspNetCore.Mvc;

namespace ToptanciTakip.ViewComponents.Default
{
    public class Slider:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
