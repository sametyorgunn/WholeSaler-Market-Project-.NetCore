using DataAccessLayer.Contexts;
using EntityLayer.Dtos;
using EntityLayer.Entities;
using EntityLayer.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ToptanciTakip.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {

            Context c = new Context();
            var roles = c.Roles.Where(x => x.RolType != ((int)UserRoleType.Admin)).ToList();
            List<SelectListItem> RoleList = (from x in roles
                                                select new SelectListItem
                                                {
                                                    Text = x.Name,
                                                    Value = x.Id.ToString()
                                                }).ToList();
            ViewBag.RoleList = RoleList;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserRegisterDto dto)
        {
            if (ModelState.IsValid)
            {
                if (dto.Password == dto.RePassword)
                {
                    Context c = new Context();
                    var roleId = c.Roles.Find(dto.RoleId);
                    var rolename = roleId.ToString();
                    AppUser user = new AppUser()
                    {
                        NameSurname = dto.NameSurname,
                        Adress = dto.Adress,
                        CompanyName = dto.CompanyName,
                        Email = dto.Mail,
                        PhoneNumber = dto.Phone,
                        TaskNo = dto.TaskNo,
                        Status = false,
                        UserName =dto.UserName
                        

                    };
                    var result = await _userManager.CreateAsync(user, dto.Password);
                    if (result.Succeeded)
                    {
                        if (roleId != null)
                        {
                            IdentityResult roleresult = await _userManager.AddToRoleAsync(user, rolename);
                            return RedirectToAction("Index", "Login");
                        }

                    }
                }
                else
                {
                    ViewBag.Hata = "Şifreler Uyuşmuyor";
                    return View();
                }
            }
            else
            {
                return View();
            }
            return View();
        }

    }
}
