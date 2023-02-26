using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;
using Payment_Simulator.Interfaces;
using Payment_Simulator.Models;

namespace Payment_Simulator.Repository
{
    public class StoreRepository : IStoreRepository
    {
        private readonly AppDbContext _context;

        public StoreRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Store store)
        {
            _context.Add(store);
            _context.SaveChanges();
        }

        public void Delete(Store store)
        {
            _context.Remove(store);
            _context.SaveChanges();
        }

        public Store GetByID(int Id)
        {
            var store = _context.Stores.FirstOrDefault(x => x.Id == Id);
            if (store != null)
            {
                return store;
            }
            return null;
        }

        public IQueryable<Store> GetAll()
        {
            return _context.Stores;
        }

        public void Update(Store store)
        {
            _context.Entry(store).State = EntityState.Modified;
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
