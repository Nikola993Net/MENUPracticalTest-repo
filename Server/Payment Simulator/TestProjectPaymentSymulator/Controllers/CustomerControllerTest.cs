using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Payment_Simulator.Controllers;
using Payment_Simulator.Interfaces;
using Payment_Simulator.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectPaymentSymulator.Controllers
{
    public class CustomerControllerTest
    {
        [Fact]
        public void CheckCustomerAccountBalance_ValidId_ReturnsObject()
        {
            // Arrange
            Customer customer = new Customer()
            {
                Id = 1,
                Username = "Marko93",
                CustomerAccountBalance = 25000,
                StoreId = 1,
                Store = new Store() { Id = 1, StoreAccountBalance = 50000, LastTransactionId = 10 }
            };


            var mockRepository = new Mock<ICustomerRepository>();
            mockRepository.Setup(x => x.GetByUserName(customer.Username)).Returns(customer);
            var mockRepository1 = new Mock<IStoreRepository>();

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new CustomerProfile()));
            IMapper mapper = new Mapper(mapperConfiguration);


            var controller = new CustomersController(mockRepository.Object, mapper, mockRepository1.Object);

            // Act
            var actionResult = controller.CheckCustomerAccountBalance(customer.Username) as OkObjectResult;
            // Assert
            Assert.NotNull(actionResult);
            Assert.NotNull(actionResult.Value);
            Assert.Equal(customer.CustomerAccountBalance, (decimal)actionResult.Value);
        }

        [Fact]
        public void CheckCustomerAccountBalance_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var mockRepository = new Mock<ICustomerRepository>();
            var mockRepository1 = new Mock<IStoreRepository>();

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new CustomerProfile()));
            IMapper mapper = new Mapper(mapperConfiguration);


            var controller = new CustomersController(mockRepository.Object, mapper, mockRepository1.Object);

            // Act
            var actionResult = controller.CheckCustomerAccountBalance("Marko93") as NotFoundResult;

            // Assert
            Assert.NotNull(actionResult);
        }

        [Fact]
        public void CheckCustomerAccountBalance_ValidRequest_ReturnsObject()
        {
            // Arrange
            Store sotre = new Store() { Id = 1, StoreAccountBalance = 50000, LastTransactionId = 10 };
            Customer customer = new Customer()
            {
                Id = 1,
                Username = "Marko93",
                CustomerAccountBalance = 25000,
                StoreId = 1,
                Store = sotre
            };

            var mockRepository = new Mock<ICustomerRepository>();
            var mockRepository1 = new Mock<IStoreRepository>();

            mockRepository.Setup(x => x.GetByUserName(customer.Username)).Returns(customer);
            mockRepository1.Setup(x => x.GetByID(customer.StoreId)).Returns(sotre);
            
            mockRepository.Setup(x => x.Update(customer));
            
            mockRepository1.Setup(x => x.Update(sotre));

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new CustomerProfile()));
            IMapper mapper = new Mapper(mapperConfiguration);

            var controller = new CustomersController(mockRepository.Object, mapper, mockRepository1.Object);
            AmountInput amount = new AmountInput()
            {
                Amount = 500,
                TypeCyrrency = Currency.Din
            };
            // Act
            var actionResult = controller.Pay(customer.Username, amount) as OkObjectResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.NotNull(actionResult.Value);

            Assert.Equal(sotre.LastTransactionId, (int)actionResult.Value);
        }

        [Fact]
        public void CheckCustomerAccountBalance_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            Store sotre = new Store() { Id = 1, StoreAccountBalance = 50000, LastTransactionId = 10 };
            Customer customer = new Customer()
            {
                Id = 1,
                Username = "Marko93",
                CustomerAccountBalance = 25000,
                StoreId = 1,
                Store = sotre
            };

            var mockRepository = new Mock<ICustomerRepository>();
            var mockRepository1 = new Mock<IStoreRepository>();

            mockRepository.Setup(x => x.GetByUserName(customer.Username)).Returns(customer);
            mockRepository1.Setup(x => x.GetByID(customer.StoreId)).Returns(sotre);

            mockRepository.Setup(x => x.Update(customer));

            mockRepository1.Setup(x => x.Update(sotre));

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new CustomerProfile()));
            IMapper mapper = new Mapper(mapperConfiguration);

            var controller = new CustomersController(mockRepository.Object, mapper, mockRepository1.Object);
            AmountInput amount = new AmountInput()
            {
                Amount = 500,
                TypeCyrrency = Currency.Din
            };

            // Act
            var actionResult = controller.Pay("PeraPeric", amount) as NotFoundResult;

            // Assert
            Assert.NotNull(actionResult);
        }
    }
}
