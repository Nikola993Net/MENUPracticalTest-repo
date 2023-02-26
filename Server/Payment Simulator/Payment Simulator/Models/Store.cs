namespace Payment_Simulator.Models
{
    public class Store
    {
        public int Id { get; set; }
        public decimal StoreAccountBalance { get; set; }
        public int LastTransactionId { get; set; }
    }
}
