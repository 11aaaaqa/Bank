using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.ModelsBank;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bank.Models
{
    public class DtbContext : IdentityDbContext<User>
    {
        public DtbContext(DbContextOptions<DtbContext> options) : base(options) { }

        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<BankHistory> BankHistory { get; set; }
    }
}
