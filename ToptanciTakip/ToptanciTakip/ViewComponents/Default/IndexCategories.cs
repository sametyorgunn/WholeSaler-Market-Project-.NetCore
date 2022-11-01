using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ToptanciTakip.ViewComponents.Default
{
    public class IndexCategories:ViewComponent
    {
        private readonly ICategoryService _categoryService;

		public IndexCategories(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		public IViewComponentResult Invoke()
        {
            var category = _categoryService.GetListAll(x=>x.Status==true);
            return View(category);
        }
    }
}
