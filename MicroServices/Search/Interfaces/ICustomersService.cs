using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Search.Interfaces
{
    public interface ICustomersService
    {
        Task<(bool IsSuccess, dynamic customer, string ErrorMessage)> GetCustomerAsync(int customerId);
    }
}
