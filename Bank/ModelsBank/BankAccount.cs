using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.ModelsBank
{
    [Table("BankAccounts")]
    public class BankAccount
    {
        [Key]
        [Required]
        [Display(Name = "Номер карты")]
        [DataType(DataType.CreditCard)]
        public string CardNumber { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Сумма")]
        public ulong Money { get; set; }
    }
}
