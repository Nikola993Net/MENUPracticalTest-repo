namespace Payment_Simulator.Models
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
        public Currency TypeCyrrency { get; set; }
    }
}
