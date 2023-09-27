namespace ProvaPub.Services
{
	public class RandomService
	{
		// A semente só é necessária na classe Random se precisarmos gerar os mesmos números toda vez.
		public RandomService()
		{
		}
		public int GetRandom()
		{
			return new Random().Next(100);
		}

	}
}
