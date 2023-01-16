using ECommerce.Application.Repositories.Products;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        [HttpGet]
        public IActionResult GetProducts() => Ok(_productReadRepository.GetAll());

        [HttpPost]
        public async Task<IActionResult> AddProduct()
        {
            await _productWriteRepository.AddAsync(new Product { Name = "Test", Price = 10, Stock = 10, CreatedDate = DateTime.UtcNow });
            await _productWriteRepository.SaveChangesAsync();
            return Ok();
        }
    }
}
