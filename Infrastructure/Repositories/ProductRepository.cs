using Domain.Entities;

namespace Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    public ICollection<Product> GetAllProducts()
    {
        throw new NotImplementedException();
    }

    public ICollection<Product> FindProductByName(string name)
    {
        throw new NotImplementedException();
    }

    public Product FindProductById(Guid id)
    {
        throw new NotImplementedException();
    }

    public ICollection<Product> GetAllProductWithCategory(Category category)
    {
        throw new NotImplementedException();
    }

    public ICollection<Product> SearchInProducts()
    {
        throw new NotImplementedException();
    }
}