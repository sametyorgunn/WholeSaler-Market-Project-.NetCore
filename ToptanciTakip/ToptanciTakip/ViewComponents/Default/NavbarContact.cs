using Microsoft.AspNetCore.Mvc;

namespace ToptanciTakip.ViewComponents.Default
{
    public class NavbarContact:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
