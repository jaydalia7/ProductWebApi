using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProductWebApi.Data;
using ProductWebApi.Entities;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;

namespace ProductWebApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DbContextClass _dbContext;

        public ProductRepository(DbContextClass dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<int> AddProductAsync(Product product)
        {
            var param = new List<SqlParameter>
            {
                new SqlParameter("@ProductName", product.ProductName),
                new SqlParameter("@ProductDescription", product.ProductDescription),
                new SqlParameter("@ProductPrice", product.ProductPrice),
                new SqlParameter("@ProductStock", product.ProductStock)
            };
            var result = await _dbContext.Database.ExecuteSqlRawAsync("Exec AddNewProduct @ProductName, @ProductDescription, @ProductPrice, @ProductStock", param.ToArray());

            return result;
        }

        public async Task<int> DeleteProductAsync(int Id)
        {
            var param = new SqlParameter("@ProductId", Id);
            var result = await _dbContext.Database.ExecuteSqlRawAsync("Exec DeleteProduct @ProductId", param);
            return result;
        }

        public async Task<IEnumerable<Product>> GetProductByIdAsync(int Id)
        {
            var param = new SqlParameter("@ProductId", Id);
            var productDetails = await _dbContext.Products.FromSqlRaw("Exec GetProductByID @ProductId", param).ToListAsync();
            return productDetails;
        }

        public async Task<List<Product>> GetProductListAsync()
        {
            var productlst = await _dbContext.Products.FromSqlRaw("Exec GetProductList").ToListAsync();
            return productlst;
        }

        public async Task<int> UpdateProductAsync(Product product)
        {
            var parameter = new List<SqlParameter>
            {
                new SqlParameter("@ProductId", product.ProductId),
                new SqlParameter("@ProductName", product.ProductName),
                new SqlParameter("@ProductDescription", product.ProductDescription),
                new SqlParameter("@ProductPrice", product.ProductPrice),
                new SqlParameter("@ProductStock", product.ProductStock)
            };
            var result = await _dbContext.Database.ExecuteSqlRawAsync("Exec UpdateProduct @ProductId, @ProductName, @ProductDescription, @ProductPrice, @ProductStock", parameter.ToArray());
        
            return result;
        }
    }
}
