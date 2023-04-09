using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.ModelsBank
{
    public class BankHistory
    {
        public Guid Id { get; set; }
        public string Status { get; set; }

        public DateTime Time { get; set; }

        [DataType(DataType.CreditCard)]
        public string FromCardNumber { get; set; }

        [DataType(DataType.CreditCard)]
        public string ToCardNumber { get; set; }

        public long Amount { get; set; }
    }
}
