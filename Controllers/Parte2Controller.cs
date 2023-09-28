using Microsoft.AspNetCore.Mvc;
using ProvaPub.Interfaces;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;

namespace ProvaPub.Controllers
{
	
	[ApiController]
	[Route("[controller]")]
	public class Parte2Controller : ControllerBase
	{
		/// <summary>
		/// 
		/// Foi criado um service padrão para retornar listagens paginadas, dispensando a utilização de códigos repetidos.
		/// Foi criada uma interface para esse service, para utilizar Injeção de Dependência.
		/// O parâmetro page não estava sendo utilizado no service, por isso a listagem vinha sempre igual.
		/// 
		/// </summary>
		
		private readonly IPaginationService _paginationService;
        private readonly IServiceScopeFactory _scopeFactory;

        public Parte2Controller(
			IPaginationService paginationService, 
			IServiceScopeFactory scopeFactory)
		{
			_paginationService = paginationService ??
				throw new ArgumentNullException(nameof(paginationService));
			_scopeFactory = scopeFactory;
		}
	
		[HttpGet("products")]
		public dynamic ListProducts(int page)
		{
            var scope = _scopeFactory.CreateScope();
            var ctx = scope.ServiceProvider.GetRequiredService<TestDbContext>();

			List<dynamic> products = ctx.Products.ToList<dynamic>();

            return _paginationService.List(products, page);
		}

		[HttpGet("customers")]
		public dynamic ListCustomers(int page)
		{
            var scope = _scopeFactory.CreateScope();
            var ctx = scope.ServiceProvider.GetRequiredService<TestDbContext>();

            List<dynamic> customers = ctx.Customers.ToList<dynamic>();

            return _paginationService.List(customers, page);
		}
	}
}
