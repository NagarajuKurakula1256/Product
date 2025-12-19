using MyApp.Models;

namespace MyApp.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product? GetById(int id);
        Product Add(Product product);
        bool Update(Product product);
        bool Delete(int id);
    }
}