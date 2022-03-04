using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Search.Models;

namespace Search.Interfaces
{
    public interface IProductsService
    {
        Task<(bool IsSuccess, IEnumerable<Product> Products, string ErrorMessage)> GetProductsAsync();
    }
}
