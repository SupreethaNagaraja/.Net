using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Products.DB;
using Products.Interfaces;

namespace Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDBContext _dbContext;
        private readonly ILogger<ProductsProvider> _logger; 
        private readonly IMapper _mapper;
        public ProductsProvider(ProductsDBContext dbContext, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._logger = logger;
            this._mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if(!_dbContext.Products.Any())
            {
                _dbContext.Products.Add(new DB.Product() { Id = 1, Name = "Keyboard", Price = 2000, Inventory = 100 });
                _dbContext.Products.Add(new DB.Product() { Id = 2, Name = "Mouse", Price = 1000, Inventory = 50 });
                _dbContext.Products.Add(new DB.Product() { Id = 3, Name = "Monitor", Price = 20000, Inventory = 100 });
                _dbContext.Products.Add(new DB.Product() { Id = 4, Name = "CPU", Price = 10000, Inventory = 100 });
                _dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Product> Products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await _dbContext.Products.ToListAsync();
                if(products != null && products.Any())
                {
                    var result = _mapper.Map<IEnumerable<DB.Product>, IEnumerable<Models.Product>>(products);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Models.Product Product, string ErrorMessage)> GetProductAsync(int id)
        {
            try
            {
                var product = await _dbContext.Products.FirstOrDefaultAsync( p=>p.Id == id);
                if (product != null)
                {
                    var result = _mapper.Map<DB.Product, Models.Product>(product);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
