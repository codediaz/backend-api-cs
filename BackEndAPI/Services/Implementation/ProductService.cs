// Extends Models, services and plugings
using BackEndAPI.Models;
using BackEndAPI.Services;
using BackEndAPI.Services.Contract;
using Microsoft.EntityFrameworkCore;

namespace BackEndAPI.Services.Implementation
{
    public class ProductService : IProductServices
    {
        
        private readonly DBSupermarketContext _dbContext;

        //DBSupermarket context to encapsule connection
        public ProductService(DBSupermarketContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Tasks in service to consume
        public async Task<List<Product>> GetList()
        {
            try
            {
                List<Product> productList = new List<Product>();
                productList = await _dbContext.Products.ToListAsync();

                return productList;

            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Product> Get(int idItem)
        {
            try
            {
                Product? productFound = new Product();
                productFound = await _dbContext.Products
                    .Where(e => e.IdItem == idItem)
                    .FirstOrDefaultAsync();

                return productFound;

            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Product> Add(Product model)
        {
            try
            {
                _dbContext.Products.Add(model);
                await _dbContext.SaveChangesAsync();

                return model;

            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Product> Update(Product model)
        {
            _dbContext.Products.Update(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<bool> Delete(Product model)
        {
            try
            {
                _dbContext.Products.Remove(model);
                await _dbContext.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
    }
}
