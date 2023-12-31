﻿using Microsoft.EntityFrameworkCore;
using ProvaPub.Interfaces;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
    public class CustomerService
    {
        private readonly TestDbContext _testDbContext;

        public CustomerService(TestDbContext testDbContext)
        {
            _testDbContext = testDbContext;
        }

        public bool CanPurchase(int customerId, decimal purchaseValue)
        {
            if (customerId <= 0) 
                throw new ArgumentOutOfRangeException(nameof(customerId));

            if (purchaseValue <= 0) 
                throw new ArgumentOutOfRangeException(nameof(purchaseValue));

            //Business Rule: Non registered Customers cannot purchase
            var customer = _testDbContext.Customers.Find(customerId);
            if (customer == null) throw new InvalidOperationException($"Customer Id {customerId} does not exists");

            //Business Rule: A customer can purchase only a single time per month
            var baseDate = DateTime.UtcNow.AddMonths(-1);
            var ordersInThisMonth = _testDbContext.Orders.Local.Count(s => s.CustomerId == customerId && s.OrderDate >= baseDate);
            if (ordersInThisMonth > 0)
                return false;

            //Business Rule: A customer that never bought before can make a first purchase of maximum 100,00
            var haveBoughtBefore = _testDbContext.Customers.Count(s => s.Id == customerId && s.Orders.Any());
            if (haveBoughtBefore == 0 && purchaseValue > 100)
                return false;

            return true;
        }
    }
}
