using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Customers.DB;
using Customers.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Customers.Providers
{
    public class CustomersProvider : ICustomersProvider
    {
        private readonly ILogger<CustomersProvider> _logger;
        private readonly IMapper _mapper;
        private readonly CustomersDBContext _dbContext;

        public CustomersProvider(ILogger<CustomersProvider> logger, IMapper mapper, CustomersDBContext dbContext)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._dbContext = dbContext;

            SeedData();
        }

        private void SeedData()
        {
            if (!_dbContext.Customers.Any())
            {
                _dbContext.Customers.Add(new DB.Customer() { Id = 1, Name = "Supreetha", Address = "Bangalore" });
                _dbContext.Customers.Add(new DB.Customer() { Id = 2, Name = "Ritu", Address = "Bangalore" });
                _dbContext.Customers.Add(new DB.Customer() { Id = 3, Name = "Chandru", Address = "Bangalore" });
                _dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, Models.Customer Customer, string errorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var customer = _dbContext.Customers.FirstOrDefault(c => c.Id == id);
                if (customer != null)
                {
                    var result = _mapper.Map<DB.Customer, Models.Customer>(customer);
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Customer> Customers, string errorMessage)> GetCustomersAsync()
        {
            try
            {
                var customers = await _dbContext.Customers.ToListAsync();
                if (customers != null && customers.Any())
                {
                    var result = _mapper.Map<IEnumerable<DB.Customer>, IEnumerable<Models.Customer>>(customers);
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
