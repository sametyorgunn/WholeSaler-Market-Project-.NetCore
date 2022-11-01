using BusinessLayer.Abstract;
using DataAccessLayer.Contexts;
using EntityLayer.Dtos;
using EntityLayer.Entities;
using EntityLayer.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ToptanciTakip.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISucribeMailService _sucribeMailservice;
        private readonly IProductService _productService;
        private readonly IProductRequestService _productRequestService;

        public HomeController(ILogger<HomeController> logger, ISucribeMailService sucribeMailservice, IProductService productService, IProductRequestService productRequestService)
        {
            _logger = logger;
            _sucribeMailservice = sucribeMailservice;
            _productService = productService;
            _productRequestService = productRequestService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public PartialViewResult SucribeMail()
		{
            return PartialView();
		}
		[HttpPost]
        public PartialViewResult SucribeMail(SucribeMail suc)
        {
            if(ModelState.IsValid)
			{
                _sucribeMailservice.TAdd(suc);
                Response.Redirect("/Home/Index", true);
			}
            else
			{
                ViewBag.hata = "Gönderilemedi";
			}
            return PartialView();
           
        }

        public IActionResult Wholesalers()
		{
            using(var c = new Context())
			{
                var roles = c.Roles.Where(x => x.RolType == ((int)UserRoleType.Wholesaler)).Select(y => y.Id).ToList();
                var roless = c.UserRoles.Where(x => roles.Contains(x.RoleId)).Select(y => y.UserId).ToList();
                var wholesalers = c.Users.Where(x => roless.Contains(x.Id)).ToList();
                return View(wholesalers);
			}
		}
        public IActionResult WholesalersCategories(int id)
        {

            Context c = new Context();
            var categories = c.Products.Where(x => x.AppUserId == id).Include(z=>z.Category)
                .Select(y => y.Category).Distinct().ToList();
            return View(categories);
        }
        public IActionResult WholesalersProducts(int id)
        {
            var products = _productService.GetListAll(x=>x.CategoryId==id);
            return View(products);
        }

        public IActionResult CategoryDetail(int id)
		{
            //var productbycategory = _productService.GetList(); //filter gönderilecek 
            HttpContext.Session.SetInt32("CategoryId", id);
            using(var c = new Context())
			{
                var products = c.Products.Where(x => x.CategoryId == id).ToList();
                var productswithwholesalers = c.Products.Include(x=>x.AppUser).Where(x => x.CategoryId == id).ToList();

                return View(products);

            }
        }

        public IActionResult Sepet()
        {
            Context c = new Context();
            var username = User.Identity.Name;
            
            var userId = c.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();
            var sepetim = _productRequestService.GetListAll(x => x.MarketId == userId && x.Status==false);

            return View(sepetim);
        }
        [HttpPost]
        public IActionResult Sepeti(List<SepetDto> product)
        {
            Context c = new Context();

            foreach(var i in product)
            {
                var request = c.ProductRequests.Find(i.Id);
                request.Status = true;
                request.Quantity = i.Quantity;
                _productRequestService.TUpdate(request);
                
            }
            return Redirect("/Home/Sepet");
        }
        public IActionResult ProductRequest(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var CategoryId = HttpContext.Session.GetInt32("CategoryId");
                Context c = new Context();
                //var product = c.Products.Where(x => x.Id == productid).FirstOrDefault();
                var product = c.Products.Where(x => x.Id == id).FirstOrDefault();

                var username = User.Identity.Name;
                var user = c.Users.Where(x => x.UserName == username).FirstOrDefault();
                var wholesalerId = c.Products.Where(x => x.Id == id).Select(y => y.AppUserId).FirstOrDefault();
                var productPrice = Convert.ToInt32(product.Price);

                ProductRequests req = new ProductRequests();
                req.MarketId = user.Id;
                req.ProductId = /*Convert.ToInt32(productid);*/ id;
                req.WholesalerId = wholesalerId;
                req.MarketAdress = user.Adress;
                req.MarketName = user.CompanyName;
                req.MarketPhone = user.PhoneNumber;
                req.ProductName = product.Name;
                req.Status = false;
                req.ImagePath = product.ImagePath;
                req.Price = productPrice;
                c.ProductRequests.Add(req);
                c.SaveChanges();
                return Redirect("/Home/Sepet/" + /*productid*/id);

            }
            else
            {
                return Redirect("/Login/Index");
            }

        }
  
      
    }
}