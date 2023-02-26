using Microsoft.EntityFrameworkCore;
using Payment_Simulator.Interfaces;
using Payment_Simulator.Models;
using System;

namespace Payment_Simulator.Repository
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Customer customer)
        {
            _context.Add(customer);
            _context.SaveChanges();
        }

        public void Delete(Customer customer)
        {
            _context.Remove(customer);
            _context.SaveChanges();
        }

        public IQueryable<Customer> GetAll()
        {
            return _context.Customers;
        }

        public Customer GetByUserName(string username)
        {
            var customer = _context.Customers.FirstOrDefault(x => x.Username == username);
            if (customer != null)
            {
                return customer;
            }
            return null;
        }

        public void Update(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}
