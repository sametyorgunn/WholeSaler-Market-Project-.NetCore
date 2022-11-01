using BusinessLayer.Abstract;
using DataAccessLayer.Contexts;
using EntityLayer.Dtos;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ToptanciTakip.Areas.wholesaler.Controllers
{
    [Area("wholesaler")]
    [Authorize(Roles ="Wholesaler")]
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _webhost;
        private readonly IProductRequestService _productRequestService;

        public HomeController(ICategoryService categoryService, IProductService productService, UserManager<AppUser> userManager, IWebHostEnvironment webhost, IProductRequestService productRequestService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _userManager = userManager;
            _webhost = webhost;
            _productRequestService = productRequestService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddProduct()
        {

            List<SelectListItem> categories = (from x in _categoryService.GetList()
                                               select new SelectListItem
                                               {
                                                   Text = x.Name,
                                                   Value = x.Id.ToString()
                                               }).ToList();
            ViewBag.categories = categories;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            Context c = new Context();
            var username = User.Identity.Name;
            var userId = c.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();

            string wwwRootPath = _webhost.WebRootPath;
            string filename = Path.GetFileNameWithoutExtension(product.Image.FileName);
            string extension = Path.GetExtension(product.Image.FileName);
            product.ImagePath = filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath + "/Images/ProductImage/", filename);
            using (var filestream = new FileStream(path, FileMode.Create))
            {
                await product.Image.CopyToAsync(filestream);
            }
            product.AppUserId = userId;
            product.Status = false;
            _productService.TAdd(product);
            return Redirect("/wholesaler/Home/AddProduct");
           
        }
        public IActionResult CategoryList()
        {
            var category = _categoryService.GetList();
            return View(category);
        }
        public IActionResult ProductsList(int id)
        {
            Context c = new Context();
            var username = User.Identity.Name;
            var userId = c.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();
            var products = _productService.GetListAll(x => x.CategoryId == id && x.AppUserId==userId);
            return View(products);
        }

        public IActionResult ProductActive(int id)
        {
           
                var product = _productService.TGetById(id);
                product.Status = true;
                _productService.TUpdate(product);
                return Redirect("/wholesaler/Home/ProductsList/"+id);
               
            
        }
        public IActionResult ProductPasive(int id)
        {
            var product = _productService.TGetById(id);
            product.Status = false;
            _productService.TUpdate(product);
            return Redirect("/wholesaler/Home/ProductsList/"+id);
           

        }
        public IActionResult ProductRequests()
        {
            Context c = new Context();
            var username = User.Identity.Name;
            var userId = c.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();
           
            var requests = _productRequestService.GetListAll(x => x.WholesalerId == userId && x.Status==true);
            
            return View(requests);
        }

    }
}
