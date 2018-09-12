using CsvImport.Data;
using CsvImport.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CsvImport.Product.Repositories
{
    public class ProductRepository : RepositoryBase<ProductDbContext>, IProductRepository
    {
        public ProductRepository(IUnitOfWork<ProductDbContext> unitOfWork) : base(unitOfWork)
        {
        }

        public void Create(Product product)
        {
            UnitOfWork.Context.Add(product);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var productToDelete = await UnitOfWork.Context.Products.SingleOrDefaultAsync(x => x.Id == id);
            if (productToDelete == null)
                return;

            UnitOfWork.Context.Remove(productToDelete);
        }

        public async Task<Product> FindByIdAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var product = await UnitOfWork.Context.Products
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
            return product;
        }

        public async Task<Product> FindBySkuAsync(string sku, CancellationToken cancellationToken = default(CancellationToken))
        {
            var product = await UnitOfWork.Context.Products
                .SingleOrDefaultAsync(x => x.Sku == sku, cancellationToken);
            return product;
        }

        public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var products = await UnitOfWork.Context.Products.ToListAsync(cancellationToken);
            return products;
        }

        public void Update(Product product)
        {
            UnitOfWork.Context.Attach(product);
        }
    }
}
