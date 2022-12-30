//Import Models
using BackEndAPI.Models;

namespace BackEndAPI.Services.Contract
{
    public interface IProductServices
    {
        //Create services to use
        Task<List<Product>> GetList();
        Task<Product> Get(int idItem);
        Task<Product> Add(Product model);
        Task<Product> Update(Product model);
        Task<bool> Delete(Product model);

    }
}
