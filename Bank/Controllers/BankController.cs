using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Models;
using Bank.ModelsBank;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bank.Controllers
{
    public class BankController : Controller
    {
        private DtbContext context;

        public BankController(DtbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Transfer()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Transfer(BankAccount account)
        {
            if (ModelState.IsValid)
            {
                var cardNumber = User.Identity.Name;
                var fromAccount = context.BankAccounts.Single(x => x.CardNumber == cardNumber);
                BankAccount toAccount = null;
                try
                {
                    toAccount = context.BankAccounts.Single(x => x.CardNumber == account.CardNumber);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty,"Ошибка!");
                    return View();
                }


                if (fromAccount.Money >= account.Money)
                {
                    fromAccount.Money -= account.Money;
                    toAccount.Money += account.Money;

                    BankHistory history = new BankHistory{FromCardNumber = fromAccount.CardNumber, Status = "Перевод", Time = DateTime.Now, ToCardNumber = toAccount.CardNumber, Amount = (long)account.Money };
                    context.BankHistory.Add(history);

                    context.SaveChanges();

                    return RedirectToAction("History");
                }
                else
                {
                    ModelState.AddModelError("", "На Вашем счете не достаточно средств!");
                }
                
            }
            return View();
        }

        [HttpGet]
        public IActionResult Withdraw()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Withdraw(BankAccount account)
        {
            if (account.Money > 0)
            {
                var cardNumber = User.Identity.Name;
                var thisAccount = context.BankAccounts.Single(x => x.CardNumber == cardNumber);
                if (thisAccount.Money >= account.Money)
                {
                    thisAccount.Money -= account.Money;
                    BankHistory history = new BankHistory { FromCardNumber = thisAccount.CardNumber, Status = "Снятие", Time = DateTime.Now, Amount = (long)account.Money};
                    context.BankHistory.Add(history);
                    context.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "На вашем счете недостаточно средств");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Ошибка!");
                return View();
            }
            return RedirectToAction("History");
        }

        [HttpGet]
        public IActionResult Put()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Put(BankAccount account)
        {
            if (account.Money > 0)
            {
                var cardNumber = User.Identity.Name;
                var thisAccount = context.BankAccounts.Single(v => v.CardNumber == cardNumber);
                thisAccount.Money += account.Money;
                BankHistory history = new BankHistory { FromCardNumber = thisAccount.CardNumber, Status = "Пополнение", Time = DateTime.Now, Amount = (long)account.Money};
                context.BankHistory.Add(history);
                context.SaveChanges();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Ошибка!");
                return View();
            }

            return RedirectToAction( "History");
        }

        [Authorize]
        [HttpGet]
        public IActionResult History()
        {
            var identityName = User.Identity.Name;
            var history = context.BankHistory.Where(x => x.FromCardNumber == identityName).OrderByDescending(x => x.Time);

            return View(history);
        }

        [Authorize]
        [HttpGet]
        public IActionResult IncomingTransfers()
        {
            var identityName = User.Identity.Name;
            var historyToMe = context.BankHistory.Where(x => x.ToCardNumber == identityName)
                .OrderByDescending(x => x.Time);

            return View(historyToMe);
        }
    }
}
