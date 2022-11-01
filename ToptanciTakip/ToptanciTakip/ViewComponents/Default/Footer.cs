using Microsoft.AspNetCore.Mvc;

namespace ToptanciTakip.ViewComponents.Default
{
    public class Footer:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
