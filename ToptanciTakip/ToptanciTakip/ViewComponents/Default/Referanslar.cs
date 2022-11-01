using Microsoft.AspNetCore.Mvc;

namespace ToptanciTakip.ViewComponents.Default
{
	public class Referanslar:ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
