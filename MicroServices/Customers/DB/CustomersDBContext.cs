using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Customers.DB
{
    public class CustomersDBContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public CustomersDBContext(DbContextOptions options) : base(options)
        {

        }
    }
}
