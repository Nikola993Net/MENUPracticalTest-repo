using Payment_Simulator.Models;
using System;

namespace Payment_Simulator.Interfaces
{
    public interface ICustomerRepository
    {
        IQueryable<Customer> GetAll();
        Customer GetByUserName(string username);
        void Add(Customer customer);
        void Update(Customer customer);
        void Delete(Customer customer);
    }
}
