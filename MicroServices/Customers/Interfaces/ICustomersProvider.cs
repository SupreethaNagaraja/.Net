using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customers.Models;

namespace Customers.Interfaces
{
    public interface ICustomersProvider
    {
        Task<(bool IsSuccess, IEnumerable<Customer> Customers, string errorMessage)> GetCustomersAsync();

        Task<(bool IsSuccess, Customer Customer, string errorMessage)> GetCustomerAsync(int id);
    }
}
