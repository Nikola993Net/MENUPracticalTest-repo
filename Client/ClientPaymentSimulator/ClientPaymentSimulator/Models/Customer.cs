using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ClientPaymentSimulator.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public decimal CustomerAccountBalance { get; set; }
        public int StoreId { get; set; }
    }


}
