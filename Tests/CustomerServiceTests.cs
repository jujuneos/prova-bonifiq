using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;
using Xunit;

namespace ProvaPub.Tests
{
    public class CustomerServiceTests
    {
        [Fact]
        public void IdOutOfRange()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;

            var context = new TestDbContext(options);

            context.Customers.Add(new Customer { Id = 1, Name = "Teste1" });
            context.Customers.Add(new Customer { Id = 2, Name = "Teste2" });
            context.Customers.Add(new Customer { Id = 3, Name = "Teste3" });

            var customerService = new CustomerService(context);

            // Act

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => customerService.CanPurchase(-2, 40));
            context.Dispose();
        }

        [Fact]
        public void ValueOutOfRange()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;

            var context = new TestDbContext(options);

            context.Customers.Add(new Customer { Id = 1, Name = "Teste1" });
            context.Customers.Add(new Customer { Id = 2, Name = "Teste2" });
            context.Customers.Add(new Customer { Id = 3, Name = "Teste3" });

            var customerService = new CustomerService(context);

            // Act

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => customerService.CanPurchase(2, -40));
            context.Dispose();
        }

        [Fact]
        public void CustomerNotFound()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;

            var context = new TestDbContext(options);

            context.Customers.Add(new Customer { Id = 1, Name = "Teste1" });
            context.Customers.Add(new Customer { Id = 2, Name = "Teste2" });
            context.Customers.Add(new Customer { Id = 3, Name = "Teste3" });

            var customerService = new CustomerService(context);

            // Act

            // Assert
            Assert.Throws<InvalidOperationException>(() => customerService.CanPurchase(5, 40));
            context.Dispose();
        }

        [Fact]
        public void SingleTimePerMonthReturnFalse()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;

            var context = new TestDbContext(options);

            context.Customers.Add(new Customer { Id = 1, Name = "Teste1" });
            context.Customers.Add(new Customer { Id = 2, Name = "Teste2" });
            context.Customers.Add(new Customer { Id = 3, Name = "Teste3" });

            // Cadastra um pedido feito pelo cliente 2 dez dias atrás.
            context.Orders.Add(new Order { Id = 1, CustomerId = 2, Value = 30, OrderDate = DateTime.UtcNow.AddDays(-10) });

            var customerService = new CustomerService(context);

            // Act
            var result = customerService.CanPurchase(2, 40);

            // Assert
            Assert.False(result);
            context.Dispose();
        }

        [Fact]
        public void FirstBuyValueLimitReturnFalse()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;

            var context = new TestDbContext(options);

            context.Customers.Add(new Customer { Id = 1, Name = "Teste1" });
            context.Customers.Add(new Customer { Id = 2, Name = "Teste2" });
            context.Customers.Add(new Customer { Id = 3, Name = "Teste3" });

            var customerService = new CustomerService(context);

            // Act
            var result = customerService.CanPurchase(3, 150);

            // Assert
            Assert.False(result);
            context.Dispose();
        }

        [Fact]
        public void CanBuyReturnTrue()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;

            var context = new TestDbContext(options);

            context.Customers.Add(new Customer { Id = 1, Name = "Teste1" });
            context.Customers.Add(new Customer { Id = 2, Name = "Teste2" });
            context.Customers.Add(new Customer { Id = 3, Name = "Teste3" });

            var customerService = new CustomerService(context);

            // Act
            var result = customerService.CanPurchase(3, 10);

            // Assert
            Assert.True(result);
            context.Dispose();
        }
    }
}
