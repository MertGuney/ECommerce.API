﻿using ECommerce.Application.Repositories.Products;
using ECommerce.Application.ViewModels.Products;
using ECommerce.Domain.Entities;
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
        public IActionResult GetProducts() => Ok(_productReadRepository.GetAll(false));

        [HttpGet("{id}")]
        public IActionResult GetProductById(string id) => Ok(_productReadRepository.GetByIdAsync(id, false));

        [HttpPost]
        public async Task<IActionResult> AddProduct(CreateProductViewModel model)
        {
            await _productWriteRepository.AddAsync(new() { Name = model.Name, Price = model.Price, Stock = model.Stock });
            await _productWriteRepository.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductViewModel model)
        {
            var product = await _productReadRepository.GetByIdAsync(model.Id);
            product.Stock = model.Stock;
            product.Name = model.Name;
            product.Price = model.Price;
            await _productWriteRepository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveChangesAsync();
            return NoContent();
        }
    }
}
