using System;
using System.Linq;
using System.Threading.Tasks;
using Bank.Models;
using Bank.ModelsBank;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bank.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly DtbContext context;

        public AccountController(UserManager<User> _userManager, SignInManager<User> _signInManager, DtbContext _context)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            context = _context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Register model)
        {
            if (ModelState.IsValid)
            {
                Random:
                Random random = new Random();
                int length = 15;
                string cardNumber = null;
                for (int i = 0; i < 1; i++)
                {
                    cardNumber += ((random.Next(1, 9))).ToString();
                }
                for (int i = 0; i < length; i++)
                {
                    cardNumber += ((random.Next(0, 9))).ToString();
                }


                var ifcardNumber = context.Users.FirstOrDefault(u => u.CardNumber == cardNumber);
                if (ifcardNumber != null)
                    goto Random;

                var email = context.Users.FirstOrDefault(u => u.Email == model.Email);
                var phoneNumber = context.Users.FirstOrDefault(u => u.PhoneNumber == model.TelephoneNumber);
                if (email == null)
                {
                    if (phoneNumber == null)
                    {
                        User user = new User { Email = model.Email, UserName = cardNumber, DateOfBirth = model.DateOfBirth, PhoneNumber = model.TelephoneNumber, CardNumber = cardNumber };
                        var result = await userManager.CreateAsync(user, model.Password);
                        if (result.Succeeded)
                        {
                            BankAccount account = new BankAccount{CardNumber = cardNumber, Money = 0, PhoneNumber = model.TelephoneNumber};
                            context.Entry(account).State = EntityState.Added;
                            context.SaveChanges();
                            await signInManager.SignInAsync(user, false);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Этот номер телефона уже используется");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Эта электронная почта уже используется");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new Login{ReturnUrl = returnUrl});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await signInManager.PasswordSignInAsync(model.CardNumber, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
