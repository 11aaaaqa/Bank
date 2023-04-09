﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bank.Models;

namespace Bank.Controllers
{
    public class HomeController : Controller
    {
        public DtbContext context;

        public HomeController(DtbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var identityName = User.Identity.Name;
            var thisAcc = context.BankAccounts.Where(x => x.CardNumber == identityName).OrderBy(x => x.CardNumber);
            return View(thisAcc);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
