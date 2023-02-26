using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientPaymentSimulator
{
    public enum Currency
    {
        Din = 1,
        Eur = 2,
        Usd = 3
    }
    public class AmountInput
    {
        public decimal Amount { get; set; }
        public int TypeCyrrency { get; set; }
    }
}
