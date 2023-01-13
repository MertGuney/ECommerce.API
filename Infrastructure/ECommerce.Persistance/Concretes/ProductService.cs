using ECommerce.Application.Abstractions;
using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistance.Concretes
{
    public class ProductService : IProductService
    {
        public List<Product> GetProducts() => new() { new Product { Id = Guid.NewGuid(), Name = "Pen", Price = 10, Stock = 10 } };
    }
}
