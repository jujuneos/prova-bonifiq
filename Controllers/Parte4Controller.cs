using Microsoft.AspNetCore.Mvc;
using ProvaPub.Interfaces;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;

namespace ProvaPub.Controllers
{
	
	/// <summary>
	/// 
	/// Foram feitos os testes utilizando o framework xUnit e o InMemory do Entity Framework Core para simular o banco de dados em memória.
	/// 
	/// </summary>
	[ApiController]
	[Route("[controller]")]
	public class Parte4Controller :  ControllerBase
	{
		CustomerService _svc;
        public Parte4Controller(CustomerService svc)
        {
			_svc = svc;
        }

		[HttpGet("CanPurchase")]
		public bool CanPurchase(int customerId, decimal purchaseValue)
		{
			return _svc.CanPurchase(customerId, purchaseValue);
		}
	}
}
