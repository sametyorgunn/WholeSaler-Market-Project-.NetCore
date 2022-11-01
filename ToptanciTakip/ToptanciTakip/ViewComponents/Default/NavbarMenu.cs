using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ToptanciTakip.ViewComponents.Default
{
    public class NavbarMenu:ViewComponent
    {
        private readonly ICategoryService _categoryService;

		public NavbarMenu(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		public IViewComponentResult Invoke()
        {
            var categories = _categoryService.GetListAll(x=>x.Status==true);
            return View(categories);
        }
    }
}
