using DataAccessLayer.Contexts;
using EntityLayer.Dtos;
using EntityLayer.Entities;
using EntityLayer.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ToptanciTakip.Areas.Yonetim.Controllers
{
    [Area("Yonetim")]
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserLoginDto dto)
        {

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, false, true);

                if (result.Succeeded)
                {
                    Context c = new Context();
                    var user = c.Users.Where(x => x.UserName == dto.UserName).FirstOrDefault();
                    var userid = user.Id;
                    var roleId = c.UserRoles.Where(x => x.UserId == userid).Select(y => y.RoleId).FirstOrDefault();
                    var roletype = c.Roles.Where(x => x.Id == roleId).Select(y => y.RolType).FirstOrDefault();

                    if (roletype == (int)UserRoleType.Admin)
                    {
                        return Redirect("/Yonetim/Home/Index");
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return View();
                }

            }
            else
            {
                return View();
            }
        }
    }
}
