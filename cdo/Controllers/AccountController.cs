using cdo.Models;
//using Attest.Views; // пространство имен моделей RegisterModel и LoginModel
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace cdo.Controllers
{
    public class AccountController : Controller
    {
        private DataContext db;

        public AccountController(DataContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {


                string password = model.Password;

                // generate a 128-bit salt using a secure PRNG
                string a = "Соль";

                byte[] salt = Encoding.Default.GetBytes(a);

                // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));


                user user = await db.User.FirstOrDefaultAsync(u => u.login == model.Email && u.pass == hashed);
                if (user != null)
                {
                    await Authenticate(model.Email); // аутентификация

                    return RedirectToAction("Lk", "Lk", new { id = user.id });
                }

                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                user user = await db.User.FirstOrDefaultAsync(u => u.login == model.Email);
                if (user == null)
                {
                    string password = model.Password;
                    string snils = model.Snils;
                    int Mo = Convert.ToInt16(model.mo_s);
                    // generate a 128-bit salt using a secure PRNG
                    string a = "Соль";

                    byte[] salt = Encoding.Default.GetBytes(a);
                    /* byte[] salt = new byte[128 / 8];
                    using (var rng = RandomNumberGenerator.Create())
                    {
                        rng.GetBytes(salt);
                    }
                   */
                    // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
                    string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: password,
                        salt: salt,
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: 10000,
                        numBytesRequested: 256 / 8));

                    // добавляем пользователя в бд
                    db.User.Add(new user { login = model.Email, pass = hashed });
                    await db.SaveChangesAsync();

                    await Authenticate(model.Email); // аутентификация

                    await db.User.FirstOrDefaultAsync(u => u.login == model.Email);
                    user = await db.User.FirstOrDefaultAsync(u => u.login == model.Email);
                    return RedirectToAction("Lk", "Lk", new { id = user.id });
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }

            return View(model);
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}