using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payment_Simulator.Interfaces;
using Payment_Simulator.Models;

namespace Payment_Simulator.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly IStoreRepository _storeRepository;

        public CustomersController(ICustomerRepository customerRepository, IMapper mapper, IStoreRepository storeRepository)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _storeRepository = storeRepository;
        }

        // GET: api/Customers?userName = ""
        [HttpGet("{userName}")]
        public IActionResult CheckCustomerAccountBalance(string userName)
        {
            var customer = _customerRepository.GetByUserName(userName);

            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer.CustomerAccountBalance);
        }

        // PUT: api/Customers/userName
        [HttpPut("{userName}")]
        public IActionResult Pay(string userName, [FromBody] AmountInput amount)
        {
            var customer = _customerRepository.GetByUserName(userName);

            if (customer == null)
            {
                return NotFound();
            }

            var store = _storeRepository.GetByID(customer.StoreId);

            if (store == null)
            {
                return NotFound();
            }
            switch (amount.TypeCyrrency)
            {
                case Currency.Din:
                    customer.CustomerAccountBalance -= amount.Amount;
                    store.StoreAccountBalance += amount.Amount;
                    store.LastTransactionId++;
                    break;
                case Currency.Eur:
                    customer.CustomerAccountBalance -= amount.Amount * (decimal)117.5;
                    store.StoreAccountBalance += amount.Amount * (decimal)117.5;
                    store.LastTransactionId++;
                    break;
                case Currency.Usd:
                    customer.CustomerAccountBalance -= amount.Amount * (decimal)112.5;
                    store.StoreAccountBalance += amount.Amount * (decimal)112.5;
                    store.LastTransactionId++;
                    break;
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _customerRepository.Update(customer);
                _storeRepository.Update(store);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(store.LastTransactionId);
        }

    }
}
