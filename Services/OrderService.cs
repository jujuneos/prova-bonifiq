using ProvaPub.Enums;
using ProvaPub.Models;

namespace ProvaPub.Services
{
	public class OrderService
	{
		public async Task<Order> PayOrder(Payment payment, int customerId)
		{
			switch (payment.PaymentType)
			{
				case PaymentType.Pix:
					// Faz pagamento...
					break;
				case PaymentType.CreditCard:
					// Faz pagamento...
					break;
				case PaymentType.PayPal:
					// Faz pagamento...
					break;
			}

			return await Task.FromResult( new Order()
			{
				Value = payment.Value,
				CustomerId = customerId,
				OrderDate = DateTime.Now
			});
		}
	}
}
