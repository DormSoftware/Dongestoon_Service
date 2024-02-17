using Domain.Entities;

namespace Infrastructure.Repositories;

public interface IProductRepository
{
    ICollection<Product> GetAllProducts();
    ICollection<Product> FindProductByName(string name);
    Product FindProductById(Guid id);
    ICollection<Product> GetAllProductWithCategory(Category category);
}