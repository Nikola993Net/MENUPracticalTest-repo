using Castle.Components.DictionaryAdapter;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace Payment_Simulator.Models
{
    public class Customer
    {

        public int Id { get; set; }
        public string Username { get; set; }
        public decimal CustomerAccountBalance { get; set; }

        public Store Store { get; set; }
        public int StoreId { get; set; }
    }
}
