using ProvaPub.Interfaces;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
    public class PaginationService : IPaginationService
    {
        public PaginationService()
        {}

        public dynamic List(List<dynamic> items, int page)
        {
            var totalCount = items.Count;
            bool hasNext = totalCount > 10;

            List<dynamic> paginatedItems = items
                .Skip((page - 1) * 10)
                .Take(10)
                .ToList();

            return new Pagination()
            {
                HasNext = hasNext,
                TotalCount = totalCount,
                Items = paginatedItems
            };
        }
    }
}
