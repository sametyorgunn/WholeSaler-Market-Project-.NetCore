using BusinessLayer.Abstract;
using DataAccessLayer.Contexts;
using EntityLayer.Dtos;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ToptanciTakip.Areas.Yonetim.Controllers
{
    [Area("Yonetim")]
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _webhost;

        public HomeController(ICategoryService categoryService, IProductService productService, UserManager<AppUser> userManager, IWebHostEnvironment webhost)
        {
            _categoryService = categoryService;
            _productService = productService;
            _userManager = userManager;
            _webhost = webhost;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryAddDto dto)
        {
            Context c = new Context();
            var username = User.Identity.Name;
            var userId = c.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();

            string wwwRootPath = _webhost.WebRootPath;
            string filename = Path.GetFileNameWithoutExtension(dto.Image.FileName);
            string extension = Path.GetExtension(dto.Image.FileName);
            dto.ImagePath = filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath + "/Images/CategoryImage/", filename);
            using (var filestream = new FileStream(path, FileMode.Create))
            {
                await dto.Image.CopyToAsync(filestream);
            }
            Category category = new Category()
            {
                Name = dto.Name,
                Description = dto.Description,
                Status = true,
                //AppUserId = userId,
                ImagePath = dto.ImagePath
            };
            _categoryService.TAdd(category);
            return Redirect("/Yonetim/Home/AddCategory");

        }
       

    }
}
