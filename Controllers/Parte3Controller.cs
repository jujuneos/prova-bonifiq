using Microsoft.AspNetCore.Mvc;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;

namespace ProvaPub.Controllers
{
	
	/// <summary>
	/// 
	/// Foi criado um Enum chamado PaymentType para cadastrar os tipos de pagamento.
	/// Também foi criada uma nova entidade chamada Payment, com as propriedades Value e PaymentType.
	/// No método PayOrder foi utilizado um switch/case com os tipos de pagamento cadastrados no enum.
	/// Dessa forma fica mais organizado e mais fácil de adicionar novos tipos sem mudar muita coisa no método PayOrder.
	/// 
	/// </summary>
	[ApiController]
	[Route("[controller]")]
	public class Parte3Controller :  ControllerBase
	{
		[HttpGet("orders")]
		public async Task<Order> PlaceOrder(Payment payment, int customerId)
		{
			return await new OrderService().PayOrder(payment, customerId);
		}
	}
}
