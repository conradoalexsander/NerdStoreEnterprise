using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSE.Catalogue.API.Models;

namespace NSE.Catalogue.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogueController : ControllerBase {
        private readonly IProductRepository _productRepository;
        
        public CatalogueController(IProductRepository productRepository) {
            _productRepository = productRepository;
        }

        // GET: api/Catalogue
        [HttpGet("products")]
        public Task<IEnumerable<Product>> Get() {
            return _productRepository.GetAll();
        }

        // GET: api/Catalogue/5
        [HttpGet("products/{id}", Name = "Get")]
        public Task<Product> Get(Guid id) {
            return _productRepository.GetById(id);
        }
    }
}