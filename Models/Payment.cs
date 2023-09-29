using ProvaPub.Enums;

namespace ProvaPub.Models
{
    public class Payment
    {
        public decimal Value { get; set; }
        public PaymentType PaymentType { get; set; }
    }
}
