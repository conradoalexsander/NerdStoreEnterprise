using System;
using Microsoft.EntityFrameworkCore;
using NSE.Catalogue.API.Models;
using NSE.Core.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NSE.Catalogue.API.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogueContext _context;

        public IUnitOfWork unitOfWork => _context;

        public ProductRepository(CatalogueContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.AsNoTracking().ToListAsync();
        }

        public async Task<Product> GetById(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
