using DataAccessLayer.Contexts;
using EntityLayer.Dtos;
using EntityLayer.Entities;
using EntityLayer.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace ToptanciTakip.Controllers
{
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

                    if (roletype == (int)UserRoleType.market)
                    {
                        return Redirect("/Market/Home/Index");
                    }
                    else if (roletype == (int)UserRoleType.Wholesaler)
                    {
                        return Redirect("/wholesaler/Home/Index");
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

        public IActionResult SifremiUnuttum()
		{
            return View();
		}
		[HttpPost]
        public IActionResult SifremiUnuttum(SifremiUnuttumDto dto)
        {
            using(var c = new Context())
			{
                var Mail = c.Users.Where(x => x.Email == dto.Mail).Select(y=>y.Email).FirstOrDefault().ToString();
                HttpContext.Session.SetString("Mail", Mail);
                if(Mail!=null)
				{
                    Random rand = new Random();
                    var sayi = rand.Next(1000, 9999).ToString();
                    HttpContext.Session.SetString("OnayKodu", sayi);
                    var email = new MimeMessage();
                    email.From.Add(MailboxAddress.Parse("swggerx@gmail.com"));
                    email.To.Add(MailboxAddress.Parse(Mail));
                    email.Subject = "Onay Kodu";
                    email.Body = new TextPart(TextFormat.Plain) { Text = sayi };

                    // send email
                    using var smtp = new SmtpClient();
                    smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    smtp.Authenticate("swggerx@gmail.com", "tltmlfchqcjelvtx");
                    smtp.Send(email);
                    smtp.Disconnect(true);

                    return RedirectToAction("OnayKodu", "Login");
                }
                else
				{
                    return View();
				}
			}
            
        }

        public IActionResult OnayKodu()
		{
            return View();
		}
		[HttpPost]
        public IActionResult OnayKodu(SifremiUnuttumDto onaykod)
        {
            var kod = HttpContext.Session.GetString("OnayKodu");
            if(kod==onaykod.OnayKodu)
			{
                return RedirectToAction("YeniSifre","Login");
			}
            else
			{
                ViewBag.hata = "Onaykodu Hatalı";
			}
            return View();
        }

        public IActionResult YeniSifre()
		{
            return View();
		}
		[HttpPost]
        public async Task<IActionResult> YeniSifre(SifremiUnuttumDto dto)
		{
            if (dto.Password == dto.RePassword)
            {
                Context c = new Context();
                var kisimail = HttpContext.Session.GetString("Mail");
                AppUser person = await _userManager.FindByEmailAsync(kisimail);
                person.PasswordHash = _userManager.PasswordHasher.HashPassword(person, dto.Password);
                IdentityResult result = await _userManager.UpdateAsync(person);
                return RedirectToAction("Index", "Login");

            }
            else
			{
                ViewBag.hata = "Şifreler Uyuşmuyor";
                return View();
			}
		}

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }

    }
}
