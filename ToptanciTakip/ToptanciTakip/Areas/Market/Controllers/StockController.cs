using BusinessLayer.Abstract;
using DataAccessLayer.Contexts;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ToptanciTakip.Areas.Market.Controllers
{
    [Area("Market")]
    [Authorize(Roles = "Market")]

    public class StockController : Controller
    {
        private readonly IStockCategoryService _stokCategoryService;
        private readonly IWebHostEnvironment _webhost;
        private readonly IStockService _stokService;

        public StockController(IStockCategoryService stokCategoryService, IWebHostEnvironment webhost, IStockService stokService)
        {
            _stokCategoryService = stokCategoryService;
            _webhost = webhost;
            _stokService = stokService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddStockCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddStockCategory(StockCategory category)
        {
            Context c = new Context();
            var username = User.Identity.Name;
            var userId = c.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();
            category.AppUserId = userId;
            category.Status = true;
            _stokCategoryService.TAdd(category);
            return Redirect("/Market/Stock/AddStockCategory");

            
            
        }

        public IActionResult StockCategories()
        {
            Context c = new Context();
            var username = User.Identity.Name;
            var userId = c.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();
            var stokcategoryliste = _stokCategoryService.GetListAll(x=>x.AppUserId==userId);
            return View(stokcategoryliste);
        }

        public IActionResult AddStockProduct()
        {
            var category = _stokCategoryService.GetList();
            List<SelectListItem> categories = (from x in category
                                               select new SelectListItem
                                               {
                                                   Text = x.Name,
                                                   Value = x.Id.ToString()
                                               }).ToList();

            ViewBag.categories = categories;
            return View();
        }
        [HttpPost]
        public async  Task<IActionResult> AddStockProduct(Stock stock)
        {
            Context c = new Context();
            var username = User.Identity.Name;
            var userId = c.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();
            stock.AppUserId = userId;
            stock.Status = true;
            _stokService.TAdd(stock);
            return Redirect("/Market/Stock/AddStockProduct");
        }

        public IActionResult ProductList(int id)
        {
            Context c = new Context();
            var username = User.Identity.Name;
            var userId = c.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();
            var product = _stokService.GetListAll(x => x.StocKCategoryId == id && x.AppUserId==userId);
            return View(product);
        }

    }
}
