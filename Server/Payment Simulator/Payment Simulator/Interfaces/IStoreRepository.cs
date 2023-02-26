using Payment_Simulator.Models;

namespace Payment_Simulator.Interfaces
{
    public interface IStoreRepository
    {
        IQueryable<Store> GetAll();
        Store GetByID(int Id);
        void Add(Store store);
        void Update(Store store);
        void Delete(Store store);
    }
}
