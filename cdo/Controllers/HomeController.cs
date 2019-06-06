using Attest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attest.Controllers
{

    public class HomeController : Controller
    {
        private DataContext db = new DataContext();



        // [Authorize]
        public IActionResult Index()
        {
            //return Content(User.Identity.Name);
            return View("index");
        }

        public IActionResult Info()
        {
            //return Content(User.Identity.Name);
            return View("info");
        }

        public IActionResult Login()
        {
            ViewBag.User = db.Users.ToList();
            return View("Login");
        }

        public IActionResult Register()
        {
            // ViewBag.User = db.Users.ToList();
            return View("Register");
        }

        /*   public IActionResult Index()
           {

               return View("index");
           }*/
        public async Task<IActionResult> Auto(string Email, string pass)
        {



            Users user = await db.Users.FirstOrDefaultAsync(p => p.Email == Email);

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
                password: pass,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            if (user != null && user.pass == hashed)
                return Redirect(Url.Action("Lk", "Lk", new { id = user.Id}));

            return View("ненорм");
        }
    }
}