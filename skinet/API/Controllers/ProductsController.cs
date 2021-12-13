using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Specifications;

namespace API.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> productsRepo;
        private readonly IGenericRepository<ProductBrand> productBrandRepo;
        private readonly IGenericRepository<ProductType> productTypeRepo;

        public ProductsController(IGenericRepository<Product> productsRepo,
         IGenericRepository<ProductBrand> productBrandRepo, 
         IGenericRepository<ProductType> productTypeRepo)
        {
            this.productsRepo = productsRepo;
            this.productBrandRepo = productBrandRepo;
            this.productTypeRepo = productTypeRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts(){

            var spec = new ProductWithTypesAndBrandsSpecification();
            var products = await productsRepo.ListAsync(spec);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id){
            var spec = new ProductWithTypesAndBrandsSpecification(id);
            return await productsRepo.GetEntityWithSpec(spec);

        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands(){
            return  Ok(await productBrandRepo.ListAllAsync());
        }
         
        [HttpGet("Type")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes(){
            return  Ok(await productTypeRepo.ListAllAsync());
        }
    }
}