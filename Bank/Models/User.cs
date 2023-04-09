using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Bank.Models
{
    public class User : IdentityUser
    {
        public DateTime DateOfBirth { get; set; }

        public string CardNumber { get; set; }
    }
}
