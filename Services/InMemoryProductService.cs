using System.Collections.Concurrent;
using MyApp.Models;

namespace MyApp.Services
{
    public class InMemoryProductService : IProductService
    {
        private readonly ConcurrentDictionary<int, Product> _items = new();
        private int _nextId = 1;

        public InMemoryProductService()
        {
            // Seed sample products
            Add(new Product { Name = "Sample A", Description = "Sample product A", Price = 9.99m });
            Add(new Product { Name = "Sample B", Description = "Sample product B", Price = 19.99m });
        }

        public IEnumerable<Product> GetAll() => _items.Values.OrderBy(p => p.Id);

        public Product? GetById(int id) => _items.TryGetValue(id, out var p) ? p : null;

        public Product Add(Product product)
        {
            var id = Interlocked.Increment(ref _nextId);
            product.Id = id;
            _items[product.Id] = Clone(product);
            return Clone(product);
        }

        public bool Update(Product product)
        {
            if (!_items.ContainsKey(product.Id)) return false;
            _items[product.Id] = Clone(product);
            return true;
        }

        public bool Delete(int id) => _items.TryRemove(id, out _);

        private static Product Clone(Product p) => new Product
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price
        };
    }
}